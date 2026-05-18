using Kutuphane.Core.Interfaces;
using Kutuphane.Core.Models;
using Kutuphane.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace Kutuphane.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public UserRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public IReadOnlyList<LibraryUser> GetAll()
    {
        const string sql = "SELECT * FROM Kullanicilar ORDER BY kullanici_ad";
        return QueryUsers(sql);
    }

    public LibraryUser? GetByTc(string tc)
    {
        const string sql = "SELECT * FROM Kullanicilar WHERE kullanici_tc = @tc";
        using var con = _connectionFactory.Create();
        using var cmd = new SqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@tc", tc);
        con.Open();
        using var reader = cmd.ExecuteReader();
        return reader.Read() ? MapUser(reader) : null;
    }

    public LibraryUser? GetById(int id)
    {
        const string sql = "SELECT * FROM Kullanicilar WHERE kullanici_ıd = @id";
        using var con = _connectionFactory.Create();
        using var cmd = new SqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@id", id);
        con.Open();
        using var reader = cmd.ExecuteReader();
        return reader.Read() ? MapUser(reader) : null;
    }

    public void Add(LibraryUser user)
    {
        const string sql = @"INSERT INTO Kullanicilar
            (kullanici_ad, kullanici_soyad, kullanici_tc, kullanici_tel, kullanici_mail, kullanici_cinsiyet, kullanici_ceza)
            VALUES (@ad, @soyad, @tc, @tel, @mail, @cinsiyet, @ceza)";

        using var con = _connectionFactory.Create();
        using var cmd = new SqlCommand(sql, con);
        AddUserParameters(cmd, user);
        con.Open();
        cmd.ExecuteNonQuery();
    }

    public void Update(LibraryUser user)
    {
        const string sql = @"UPDATE Kullanicilar SET
            kullanici_ad=@ad, kullanici_soyad=@soyad, kullanici_tc=@tc,
            kullanici_tel=@tel, kullanici_mail=@mail, kullanici_cinsiyet=@cinsiyet, kullanici_ceza=@ceza
            WHERE kullanici_ıd=@id";

        using var con = _connectionFactory.Create();
        using var cmd = new SqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@id", user.Id);
        AddUserParameters(cmd, user);
        con.Open();
        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        const string sql = "DELETE FROM Kullanicilar WHERE kullanici_ıd = @id";
        using var con = _connectionFactory.Create();
        using var cmd = new SqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@id", id);
        con.Open();
        cmd.ExecuteNonQuery();
    }

    public void AddPenalty(int userId, double amount)
    {
        const string sql = "UPDATE Kullanicilar SET kullanici_ceza = kullanici_ceza + @amount WHERE kullanici_ıd = @id";
        using var con = _connectionFactory.Create();
        using var cmd = new SqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@amount", amount);
        cmd.Parameters.AddWithValue("@id", userId);
        con.Open();
        cmd.ExecuteNonQuery();
    }

    private IReadOnlyList<LibraryUser> QueryUsers(string sql)
    {
        var list = new List<LibraryUser>();
        using var con = _connectionFactory.Create();
        using var cmd = new SqlCommand(sql, con);
        con.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            list.Add(MapUser(reader));
        return list;
    }

    private static void AddUserParameters(SqlCommand cmd, LibraryUser user)
    {
        cmd.Parameters.AddWithValue("@ad", user.Ad);
        cmd.Parameters.AddWithValue("@soyad", user.Soyad);
        cmd.Parameters.AddWithValue("@tc", user.Tc);
        cmd.Parameters.AddWithValue("@tel", user.Tel);
        cmd.Parameters.AddWithValue("@mail", user.Mail);
        cmd.Parameters.AddWithValue("@cinsiyet", user.Cinsiyet);
        cmd.Parameters.AddWithValue("@ceza", user.Ceza);
    }

    private static LibraryUser MapUser(SqlDataReader reader) => new()
    {
        Id = reader.GetInt32(reader.GetOrdinal("kullanici_ıd")),
        Ad = reader["kullanici_ad"]?.ToString() ?? "",
        Soyad = reader["kullanici_soyad"]?.ToString() ?? "",
        Tc = reader["kullanici_tc"]?.ToString() ?? "",
        Tel = reader["kullanici_tel"]?.ToString() ?? "",
        Mail = reader["kullanici_mail"]?.ToString() ?? "",
        Cinsiyet = reader["kullanici_cinsiyet"]?.ToString() ?? "",
        Ceza = reader["kullanici_ceza"] is DBNull ? 0 : Convert.ToDouble(reader["kullanici_ceza"])
    };
}
