using Core.Models;
using Npgsql;

namespace Server.Repositories.ArticleReadStatusRepository;

public class ArticleReadStatusRepo : IArticleReadStatusRepo
{
    private const string connectionString =
        @"Server=ep-long-sea-agyfr4ak-pooler.c-2.eu-central-1.aws.neon.tech;
        User Id=neondb_owner;Password=npg_rwHcEK1Li0Bs;Database=neondb";

    public async Task<bool> GetReadStatusAsync(Guid userId, int articleId)
    {
        using var db = new NpgsqlConnection(connectionString);
        await db.OpenAsync();

        var cmd = db.CreateCommand();
        cmd.CommandText = @"SELECT isread FROM articlereadstatus WHERE userid = @userId AND articleid = @articleId";
        cmd.Parameters.AddWithValue("@userId", userId);
        cmd.Parameters.AddWithValue("@articleId", articleId);

        var result = await cmd.ExecuteScalarAsync();
        return result != null && (bool)result;
    }

    public async Task SetReadStatusAsync(Guid userId, int articleId, bool isRead)
    {
        using var db = new NpgsqlConnection(connectionString);
        await db.OpenAsync();

        var cmd = db.CreateCommand();
        cmd.CommandText = 
            @"INSERT INTO articlereadstatus (userid, articleid, isread) VALUES (@userId, @articleId, @isRead) 
            ON CONFLICT (userid, articleid) DO UPDATE SET isread = @isRead";
        cmd.Parameters.AddWithValue("@userId", userId);
        cmd.Parameters.AddWithValue("@articleId", articleId);
        cmd.Parameters.AddWithValue("@isRead", isRead);
        
        await cmd.ExecuteNonQueryAsync();
    }
}

