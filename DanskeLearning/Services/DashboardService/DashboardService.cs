using Core.Models;

namespace DanskeLearning.Services.DashboardService;

public class DashboardService : IDashboardService
{
    public async Task<List<Subject>> GetSubjectsAsync()
    {
        await Task.Delay(1);

        return new List<Subject>
        {
            new Subject { subjectId = 1, title = "Accounts" },
            new Subject { subjectId = 2, title = "Cards" },
            new Subject { subjectId = 3, title = "AML" },
        };
    }
    
    
}