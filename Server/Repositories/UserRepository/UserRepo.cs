using Core.Models;
using Npgsql;

namespace Server.Repositories.UserRepository;

public class UserRepo : IUserRepo
{
    private const string connectionString =
        @"Server=ep-long-sea-agyfr4ak-pooler.c-2.eu-central-1.aws.neon.tech;
    User Id=neondb_owner;Password=npg_rwHcEK1Li0Bs;Database=neondb";

    public async Task<User?> GetUserById(string id)
    {
        using var db = new NpgsqlConnection(connectionString);
        await db.OpenAsync();

        var cmd = db.CreateCommand();
        cmd.CommandText = @"SELECT * FROM ""Users"" WHERE ""UserId"" = @id";
        cmd.Parameters.AddWithValue("@id", id);

        using var reader = await cmd.ExecuteReaderAsync();

        if (!reader.Read())
            return null;
        
        return new User
        {
            UserId = reader.GetString(reader.GetOrdinal("UserId")),
            firstName = reader.GetString(reader.GetOrdinal("firstName")),
            lastName = reader.GetString(reader.GetOrdinal("lastName")),
            email = reader.GetString(reader.GetOrdinal("email")),
            password = reader.GetString(reader.GetOrdinal("password")),
            role = reader.GetString(reader.GetOrdinal("role")),
            phoneNumber = reader.GetDouble(reader.GetOrdinal("phoneNumber")),
            userName = reader.GetString(reader.GetOrdinal("userName"))
        };
    }

    public async Task<User?> Login(string username, string password)
    {
        using var db = new NpgsqlConnection(connectionString);
        await db.OpenAsync();

        var cmd = db.CreateCommand();
        cmd.CommandText = @"SELECT * FROM ""Users"" WHERE ""userName"" = @username AND ""password"" = @password";

        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@password", password);

        using var reader = await cmd.ExecuteReaderAsync();

        if (!reader.Read())
            return null;

        return new User
        {
            UserId = reader.GetString(reader.GetOrdinal("UserId")),
            firstName = reader.GetString(reader.GetOrdinal("firstName")),
            lastName = reader.GetString(reader.GetOrdinal("lastName")),
            email = reader.GetString(reader.GetOrdinal("email")),
            password = reader.GetString(reader.GetOrdinal("password")),
            role = reader.GetString(reader.GetOrdinal("role")),
            phoneNumber = reader.GetDouble(reader.GetOrdinal("phoneNumber")),
            userName = reader.GetString(reader.GetOrdinal("userName"))
        };
    }
}