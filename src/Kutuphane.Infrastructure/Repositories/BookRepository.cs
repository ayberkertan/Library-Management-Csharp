using Kutuphane.Core.Interfaces;
using Kutuphane.Core.Models;
using Kutuphane.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace Kutuphane.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public BookRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public IReadOnlyList<Book> GetAll()
    {
        const string sql = "SELECT * FROM Kaynaklar ORDER BY kaynak_ad";
        return QueryBooks(sql, null);
    }

    public IReadOnlyList<Book> SearchByTitle(string keyword)
    {
        const string sql = "SELECT * FROM Kaynaklar WHERE kaynak_ad LIKE @kelime ORDER BY kaynak_ad";
        return QueryBooks(sql, "%" + keyword + "%");
    }

    public Book? GetById(int id)
    {
        const string sql = "SELECT * FROM Kaynaklar WHERE kaynak_id = @id";
        using var con = _connectionFactory.Create();
        using var cmd = new SqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@id", id);
        con.Open();
        using var reader = cmd.ExecuteReader();
        return reader.Read() ? MapBook(reader) : null;
    }

    public void Add(Book book)
    {
        const string sql = @"INSERT INTO Kaynaklar
            (kaynak_ad, kaynak_yazar, kaynak_yayıncı, kaynak_sayfasayısı, kaynak_basımtarihi)
            VALUES (@ad, @yazar, @yayinci, @sayfa, @tarih)";

        using var con = _connectionFactory.Create();
        using var cmd = new SqlCommand(sql, con);
        AddBookParameters(cmd, book);
        con.Open();
        cmd.ExecuteNonQuery();
    }

    public void Update(Book book)
    {
        const string sql = @"UPDATE Kaynaklar SET
            kaynak_ad=@ad, kaynak_yazar=@yazar, kaynak_yayıncı=@yayinci,
            kaynak_sayfasayısı=@sayfa, kaynak_basımtarihi=@tarih
            WHERE kaynak_id=@id";

        using var con = _connectionFactory.Create();
        using var cmd = new SqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@id", book.Id);
        AddBookParameters(cmd, book);
        con.Open();
        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        const string sql = "DELETE FROM Kaynaklar WHERE kaynak_id = @id";
        using var con = _connectionFactory.Create();
        using var cmd = new SqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@id", id);
        con.Open();
        cmd.ExecuteNonQuery();
    }

    public bool IsOnLoan(int bookId)
    {
        const string sql = "SELECT COUNT(*) FROM kayitlar WHERE kitap_id = @id AND durum = 1";
        using var con = _connectionFactory.Create();
        using var cmd = new SqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@id", bookId);
        con.Open();
        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
    }

    private IReadOnlyList<Book> QueryBooks(string sql, string? likeParam)
    {
        var list = new List<Book>();
        using var con = _connectionFactory.Create();
        using var cmd = new SqlCommand(sql, con);
        if (likeParam != null)
            cmd.Parameters.AddWithValue("@kelime", likeParam);
        con.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            list.Add(MapBook(reader));
        return list;
    }

    private static void AddBookParameters(SqlCommand cmd, Book book)
    {
        cmd.Parameters.AddWithValue("@ad", book.Ad);
        cmd.Parameters.AddWithValue("@yazar", book.Yazar);
        cmd.Parameters.AddWithValue("@yayinci", book.Yayinci);
        cmd.Parameters.AddWithValue("@sayfa", book.SayfaSayisi);
        cmd.Parameters.AddWithValue("@tarih", book.BasimTarihi);
    }

    private static Book MapBook(SqlDataReader reader) => new()
    {
        Id = reader.GetInt32(reader.GetOrdinal("kaynak_id")),
        Ad = reader["kaynak_ad"]?.ToString() ?? "",
        Yazar = reader["kaynak_yazar"]?.ToString() ?? "",
        Yayinci = reader["kaynak_yayıncı"]?.ToString() ?? "",
        SayfaSayisi = reader["kaynak_sayfasayısı"] is DBNull ? 0 : Convert.ToInt32(reader["kaynak_sayfasayısı"]),
        BasimTarihi = reader["kaynak_basımtarihi"] is DBNull
            ? DateTime.Today
            : Convert.ToDateTime(reader["kaynak_basımtarihi"])
    };
}
