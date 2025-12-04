using Core.DTOs;

namespace DanskeLearning.Services.TestService;
public interface ITestService
{
    Task<TestDto?> GetTest(int subjectId);
    Task<TestSubmitResult?> Submit(TestSubmitRequest req);
    Task<bool> GetTestCompleted(Guid userId, int subjectId);
    Task SetTestCompleted(Guid userId, int subjectId, bool passed);
}