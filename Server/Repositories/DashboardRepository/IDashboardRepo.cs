using Core.Models;
namespace Server.Repositories.DashboardRepository;

public interface IDashboardRepo
{
    Task<List<Subject>>  GetAllSubjectsAsync();
}