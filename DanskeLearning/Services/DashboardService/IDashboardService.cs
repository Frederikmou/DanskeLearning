using Core.Models;
namespace DanskeLearning.Services.DashboardService;

public interface IDashboardService
{
   Task<List<Subject>>  GetSubjectsAsync();
}