using Core.Models;

namespace DanskeLearning.Services.MyGrowth;

public interface IMyGrowthService
{
    public Task<List<MyGrowth>> SubmitMonthlyAnswerAsync(int answerId);
}