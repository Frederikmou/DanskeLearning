using Core.Models;
using Npgsql;

namespace Server.Repositories.SubjectRepository;

public class SubjectRepo : ISubjectRepo
{
    
    private const string connectionString =
        @"Server=ep-long-sea-agyfr4ak-pooler.c-2.eu-central-1.aws.neon.tech;
        User Id=neondb_owner;Password=npg_rwHcEK1Li0Bs;Database=neondb";
    
    public async Task<List<Articles>> GetAllArticleByIdAsync()
    {
        var result = new List<Articles>();

        using (var dbConnection = new NpgsqlConnection(connectionString))
        {
            await dbConnection.OpenAsync();

            var command = dbConnection.CreateCommand();
            command.CommandText = @"SELECT * FROM articles;";

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var articleid = reader.GetInt32(0);
                    var title = reader.GetString(1);
                    var subjectid = reader.GetInt32(2);
                    var content  = reader.GetString(3);

                    Articles a = new Articles()
                    {
                        articleId = articleid,
                        subjectId = subjectid,
                        content = content,
                        title = title
                    };
                    result.Add(a);
                }
            }
        }
        return result;
    }
}