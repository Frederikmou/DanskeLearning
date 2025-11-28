using Core.Models;
using Npgsql;
using Server.Repositories.DashboardRepository;



namespace Server.Repositories.DashboardRepository
{
    public class DashboardRepo : IDashboardRepo
    {
        private const string connectionString =
            @"Server=ep-long-sea-agyfr4ak-pooler.c-2.eu-central-1.aws.neon.tech;
    User Id=neondb_owner;Password=npg_rwHcEK1Li0Bs;Database=neondb";

        public async Task<List<Subject>> GetAllSubjectsAsync()
        {
            var result = new List<Subject>();

            using (var dbConnection = new NpgsqlConnection(connectionString))
            {
               await  dbConnection.OpenAsync();
                
                var command = dbConnection.CreateCommand();
                command.CommandText = @"SELECT * FROM subject";

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var subjectid = reader.GetInt32(0);
                        var title  = reader.GetString(1);

                        Subject s = new Subject
                        {
                            subjectId = subjectid,
                            title = title
                        };
                        result.Add(s);
                    }
                }
            }
            return result;
        }
    }
}