using Dapper;
using WebApi.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace WebApi.Data.Repositories;

public class ProfilRepository : IProfilRepository
{
    private readonly IConfiguration _config;
    private readonly string? _connectionString;
    public ProfilRepository(IConfiguration config)
    {
        _config = config;
        _connectionString = _config.GetConnectionString("DefaultConnection");
    }

    public async Task AddProfil(Profil Profil)
    {
        using IDbConnection connection = new SqlConnection(_connectionString);
        string sql = "insert into Profil (NIK,Nama,Alamat,StatusPerkawinan) values(@NIK,@Nama,@Alamat,@StatusPerkawinan)";
        await connection.ExecuteAsync(sql, new { Profil.NIK, Profil.Nama, Profil.Alamat, Profil.StatusPerkawinan }, commandType: CommandType.Text);
    }

    public async Task UpdateProfil(Profil Profil)
    {
        using IDbConnection connection = new SqlConnection(_connectionString);
        string sql = "update Profil set NIK=@NIK,Nama=@Nama,Alamat=@Alamat,StatusPerkawinan=@StatusPerkawinan where id=@id";
        await connection.ExecuteAsync(sql, new { Profil.Id, Profil.NIK, Profil.Nama, Profil.Alamat, Profil.StatusPerkawinan }, commandType: CommandType.Text);
    }

    public async Task<IEnumerable<Profil>> GetProfil()
    {
        using IDbConnection connection = new SqlConnection(_connectionString);
        string sql = "select * from Profil";
        var people = await connection.QueryAsync<Profil>(sql, commandType: CommandType.Text);
        return people;
    }

    public async Task<Profil?> GetProfilById(int id)
    {
        using IDbConnection connection = new SqlConnection(_connectionString);
        string sql = "select * from Profil where id=@id";
        var people = await connection.QueryAsync<Profil>(sql, new { id }, commandType: CommandType.Text);
        return people.FirstOrDefault();
    }

    public async Task DeleteProfil(int id)
    {
        using IDbConnection connection = new SqlConnection(_connectionString);
        string sql = "delete from Profil where id=@id";
        var people = await connection.ExecuteAsync(sql, new { id }, commandType: CommandType.Text);
    }
}
