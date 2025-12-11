using Core.Models;
using Npgsql;

namespace Server.Repositories.LearningAssignmentRepo;

public class LearningAssignmentRepo : ILearningAssignmentRepo
{
    private const string connectionString =
        @"Server=ep-long-sea-agyfr4ak-pooler.c-2.eu-central-1.aws.neon.tech;
        User Id=neondb_owner;Password=npg_rwHcEK1Li0Bs;Database=neondb";

    public async Task<LearningAssignment> CreateAssignmentAsync(LearningAssignment assignment)
    {
        await using var dbConnection = new NpgsqlConnection(connectionString);
        await dbConnection.OpenAsync();

        await using var command = dbConnection.CreateCommand();
        command.CommandText =
            @"INSERT INTO learningassignment (userid, articleid, testid, subjectid, status, assigned, completeddate)
                                VALUES (@userid, @articleid, @testid, @subjectid, @status, @assigned, @completeddate)
                                RETURNING assignmentid";

        command.Parameters.AddWithValue("@userid", assignment.userId);
        command.Parameters.AddWithValue("@articleid", assignment.articleId ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@testid", assignment.testId ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@subjectid", assignment.subjectId);
        command.Parameters.AddWithValue("@status", assignment.status);
        command.Parameters.AddWithValue("@assigned", assignment.assigned);
        command.Parameters.AddWithValue("@completeddate", assignment.completedDate ?? (object)DBNull.Value);

        var newId = await command.ExecuteScalarAsync();
        assignment.assignmentId = Convert.ToInt32(newId);
        return assignment;
    }

    public async Task<LearningAssignment?> GetAssignmentByIdAsync(int assignmentId)
    {
        using (var dbConnection = new NpgsqlConnection(connectionString))
        {
            await dbConnection.OpenAsync();

            var command = dbConnection.CreateCommand();
            command.CommandText =
                @"SELECT assignmentid, userid, articleid, testid, subjectid, status, assigned, completeddate 
                                    FROM learningassignment 
                                    WHERE assignmentid = @assignmentId";
            command.Parameters.AddWithValue("@assignmentId", assignmentId);

            using (var reader = await command.ExecuteReaderAsync())
            {
                if (!reader.Read())
                    return null;

                var assignmentid = reader.GetInt32(0);
                var userid = reader.GetGuid(1);
                var articleid = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2);
                var testid = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3);
                var subjectid = reader.GetInt32(4);
                var status = reader.GetBoolean(5);
                var assigned = reader.GetDateTime(6);
                var completeddate = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7);

                return new LearningAssignment
                {
                    assignmentId = assignmentid,
                    userId = userid,
                    articleId = articleid,
                    testId = testid,
                    subjectId = subjectid,
                    status = status,
                    assigned = assigned,
                    completedDate = completeddate
                };
            }
        }
    }

    public async Task<List<LearningAssignment>> GetAssignmentsByUserIdAsync(Guid userId)
    {
        var result = new List<LearningAssignment>();

        using (var dbConnection = new NpgsqlConnection(connectionString))
        {
            await dbConnection.OpenAsync();

            var command = dbConnection.CreateCommand();
            command.CommandText =
                @"SELECT assignmentid, userid, articleid, testid, subjectid, status, assigned, completeddate 
                                    FROM learningassignment 
                                    WHERE userid = @userId 
                                    ORDER BY assigned DESC";
            command.Parameters.AddWithValue("@userId", userId);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var assignmentid = reader.GetInt32(0);
                    var userid = reader.GetGuid(1);
                    var articleid = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2);
                    var testid = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3);
                    var subjectid = reader.GetInt32(4);
                    var status = reader.GetBoolean(5);
                    var assigned = reader.GetDateTime(6);
                    var completeddate = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7);

                    result.Add(new LearningAssignment
                    {
                        assignmentId = assignmentid,
                        userId = userid,
                        articleId = articleid,
                        testId = testid,
                        subjectId = subjectid,
                        status = status,
                        assigned = assigned,
                        completedDate = completeddate
                    });
                }
            }
        }

        return result;
    }

    public async Task<List<LearningAssignment>> GetAllAssignmentsAsync()
    {
        var result = new List<LearningAssignment>();

        using (var dbConnection = new NpgsqlConnection(connectionString))
        {
            await dbConnection.OpenAsync();

            var command = dbConnection.CreateCommand();
            command.CommandText =
                @"SELECT assignmentid, userid, articleid, testid, subjectid, status, assigned, completeddate 
                                    FROM learningassignment 
                                    ORDER BY assigned DESC";

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var assignmentid = reader.GetInt32(0);
                    var userid = reader.GetGuid(1);
                    var articleid = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2);
                    var testid = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3);
                    var subjectid = reader.GetInt32(4);
                    var status = reader.GetBoolean(5);
                    var assigned = reader.GetDateTime(6);
                    var completeddate = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7);

                    result.Add(new LearningAssignment
                    {
                        assignmentId = assignmentid,
                        userId = userid,
                        articleId = articleid,
                        testId = testid,
                        subjectId = subjectid,
                        status = status,
                        assigned = assigned,
                        completedDate = completeddate
                    });
                }
            }
        }

        return result;
    }
    
    public async Task DeleteAssignmentAsync(int assignmentId)
    {
        await using var dbConnection = new NpgsqlConnection(connectionString);
        await dbConnection.OpenAsync();

        await using var command = dbConnection.CreateCommand();
        command.CommandText = @"DELETE FROM learningassignment WHERE assignmentid = @assignmentid";
        command.Parameters.AddWithValue("@assignmentid", assignmentId);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<List<LearningAssignment>> GetAssignmentsByStatusAsync(Guid userId, bool status)
    {
        var result = new List<LearningAssignment>();

        using (var dbConnection = new NpgsqlConnection(connectionString))
        {
            await dbConnection.OpenAsync();

            var command = dbConnection.CreateCommand();
            command.CommandText = @"SELECT assignmentid, userid, articleid, testid, subjectid, status, assigned, completeddate 
                                    FROM learningassignment 
                                    WHERE userid = @userid AND status = @status 
                                    ORDER BY assigned DESC";
            command.Parameters.AddWithValue("@userid", userId);
            command.Parameters.AddWithValue("@status", status);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var assignmentid = reader.GetInt32(0);
                    var userid = reader.GetGuid(1);
                    var articleid = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2);
                    var testid = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3);
                    var subjectid = reader.GetInt32(4);
                    var statusValue = reader.GetBoolean(5);
                    var assigned = reader.GetDateTime(6);
                    var completeddate = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7);

                    result.Add(new LearningAssignment
                    {
                        assignmentId = assignmentid,
                        userId = userid,
                        articleId = articleid,
                        testId = testid,
                        subjectId = subjectid,
                        status = statusValue,
                        assigned = assigned,
                        completedDate = completeddate
                    }
                    );
                }
            }
        }
        return result;
    }
}