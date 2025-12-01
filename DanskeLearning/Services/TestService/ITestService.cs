using Core.DTOs;

namespace DanskeLearning.Services.TestService;
public interface ITestService
{
    Task<TestDto?> GetTest(int subjectId);
    Task<TestSubmitResult?> Submit(TestSubmitRequest req);
}