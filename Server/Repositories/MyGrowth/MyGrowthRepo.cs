using System;
using System.Threading.Tasks;
using Core.Models;
using Npgsql;

namespace Server.Repositories.MyGrowth;

public class MyGrowthRepo : IMyGrowthRepo
{
    private const string connectionString =
        @"Server=ep-long-sea-agyfr4ak-pooler.c-2.eu-central-1.aws.neon.tech;
          User Id=neondb_owner;Password=npg_rwHcEK1Li0Bs;Database=neondb";

    public async Task CreateAsync(Core.Models.MyGrowth growth)
    {
        const string sql = @"
            INSERT INTO mygrowth
            (userid, month, answertext, answerdate, answerid, questionid,
             fagligudfordring, nykompetence, motivation, trivsel)
            VALUES
            (@userid, @month, @answertext, @answerdate, @answerid, @questionid,
             @faglig, @nykomp, @motivation, @trivsel);";

        await using var dbConnection = new NpgsqlConnection(connectionString);
        await dbConnection.OpenAsync();

        await using var command = dbConnection.CreateCommand();
        command.CommandText = sql;

        command.Parameters.AddWithValue("userid", growth.userId);
        command.Parameters.AddWithValue("month", growth.month);
        command.Parameters.AddWithValue("answertext", (object?)growth.answerText ?? DBNull.Value);
        command.Parameters.AddWithValue("answerdate", growth.answerDate);
        command.Parameters.AddWithValue("answerid", growth.answerId);
        command.Parameters.AddWithValue("questionid", growth.questionId);
        command.Parameters.AddWithValue("faglig", (object?)growth.FagligUdfordring ?? DBNull.Value);
        command.Parameters.AddWithValue("nykomp", (object?)growth.NyKompetence ?? DBNull.Value);
        command.Parameters.AddWithValue("motivation", (object?)growth.Motivation ?? DBNull.Value);
        command.Parameters.AddWithValue("trivsel", (object?)growth.Trivsel ?? DBNull.Value);

        await command.ExecuteNonQueryAsync();
    }
}