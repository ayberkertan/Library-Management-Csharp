using Kutuphane.Core.Constants;
using Kutuphane.Core.Interfaces;
using Kutuphane.Core.Models;
using Kutuphane.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace Kutuphane.Infrastructure.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly SqlConnectionFactory _connectionFactory;
    private readonly IBookRepository _bookRepository;

    public LoanRepository(SqlConnectionFactory connectionFactory, IBookRepository bookRepository)
    {
        _connectionFactory = connectionFactory;
        _bookRepository = bookRepository;
    }

    public IReadOnlyList<LoanRecord> GetAllLoans() => QueryLoans(includeReturned: true);

    public IReadOnlyList<LoanRecord> GetActiveLoans() => QueryLoans(includeReturned: false);

    public void LendBook(int userId, int bookId, int loanDays)
    {
        if (_bookRepository.IsOnLoan(bookId))
            throw new InvalidOperationException("Bu kitap şu anda ödünçte. Önce iade alınmalıdır.");

        const string sql = @"INSERT INTO kayitlar (kullanici_id, kitap_id, alis_tarihi, son_tarih, durum)
            VALUES (@kulId, @kitapId, @alis, @son, 1)";

        using var con = _connectionFactory.Create();
        using var cmd = new SqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@kulId", userId);
        cmd.Parameters.AddWithValue("@kitapId", bookId);
        cmd.Parameters.AddWithValue("@alis", DateTime.Now);
        cmd.Parameters.AddWithValue("@son", DateTime.Now.AddDays(loanDays));
        con.Open();
        cmd.ExecuteNonQuery();
    }

    public ReturnResult ReturnBook(int loanId)
    {
        var loan = GetLoanById(loanId);
        if (loan == null)
            return new ReturnResult { Success = false, Message = "Kayıt bulunamadı." };

        var lateDays = 0;
        double penalty = 0;
        if (loan.SonTarih.Date < DateTime.Today)
        {
            lateDays = (DateTime.Today - loan.SonTarih.Date).Days;
            penalty = lateDays * LibrarySettings.LateFeePerDay;
        }

        const string sql = "UPDATE kayitlar SET durum = 0 WHERE kayit_id = @id";
        using var con = _connectionFactory.Create();
        con.Open();

        using (var cmd = new SqlCommand(sql, con))
        {
            cmd.Parameters.AddWithValue("@id", loanId);
            cmd.ExecuteNonQuery();
        }

        if (penalty > 0)
        {
            const string penaltySql =
                "UPDATE Kullanicilar SET kullanici_ceza = kullanici_ceza + @amount WHERE kullanici_ıd = @id";
            using var penaltyCmd = new SqlCommand(penaltySql, con);
            penaltyCmd.Parameters.AddWithValue("@amount", penalty);
            penaltyCmd.Parameters.AddWithValue("@id", loan.KullaniciId);
            penaltyCmd.ExecuteNonQuery();
        }

        var message = penalty > 0
            ? $"Kitap iade edildi. {lateDays} gün gecikme — {penalty:N2} TL ceza uygulandı."
            : "Kitap iade işlemi başarıyla tamamlandı.";

        return new ReturnResult
        {
            Success = true,
            Message = message,
            LateDays = lateDays,
            PenaltyApplied = penalty
        };
    }

    private LoanRecord? GetLoanById(int loanId)
    {
        const string sql = @"SELECT k.kayit_id, k.kullanici_id, k.kitap_id, k.alis_tarihi, k.son_tarih, k.durum,
            u.kullanici_ad + ' ' + u.kullanici_soyad AS kullanici_adi, kn.kaynak_ad AS kitap_adi
            FROM kayitlar k
            JOIN Kullanicilar u ON k.kullanici_id = u.kullanici_ıd
            JOIN Kaynaklar kn ON k.kitap_id = kn.kaynak_id
            WHERE k.kayit_id = @id";

        using var con = _connectionFactory.Create();
        using var cmd = new SqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@id", loanId);
        con.Open();
        using var reader = cmd.ExecuteReader();
        return reader.Read() ? MapLoan(reader) : null;
    }

    private IReadOnlyList<LoanRecord> QueryLoans(bool includeReturned)
    {
        var where = includeReturned ? "" : " WHERE k.durum = 1";
        var sql = $@"SELECT k.kayit_id, k.kullanici_id, k.kitap_id, k.alis_tarihi, k.son_tarih, k.durum,
            u.kullanici_ad + ' ' + u.kullanici_soyad AS kullanici_adi, kn.kaynak_ad AS kitap_adi
            FROM kayitlar k
            JOIN Kullanicilar u ON k.kullanici_id = u.kullanici_ıd
            JOIN Kaynaklar kn ON k.kitap_id = kn.kaynak_id{where}
            ORDER BY k.alis_tarihi DESC";

        var list = new List<LoanRecord>();
        using var con = _connectionFactory.Create();
        using var cmd = new SqlCommand(sql, con);
        con.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            list.Add(MapLoan(reader));
        return list;
    }

    private static LoanRecord MapLoan(SqlDataReader reader) => new()
    {
        KayitId = reader.GetInt32(reader.GetOrdinal("kayit_id")),
        KullaniciId = reader.GetInt32(reader.GetOrdinal("kullanici_id")),
        KitapId = reader.GetInt32(reader.GetOrdinal("kitap_id")),
        KullaniciAdi = reader["kullanici_adi"]?.ToString() ?? "",
        KitapAdi = reader["kitap_adi"]?.ToString() ?? "",
        AlisTarihi = Convert.ToDateTime(reader["alis_tarihi"]),
        SonTarih = Convert.ToDateTime(reader["son_tarih"]),
        Durum = Convert.ToBoolean(reader["durum"])
    };
}
