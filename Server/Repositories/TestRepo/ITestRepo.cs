using Core.DTOs;

namespace Server.Repositories.TestRepo;

public interface ITestRepo
{
    Task<TestDto?> GetTestBySubjectAsync(int subjectId);
    Task<TestSubmitResult> SubmitTestAsync(TestSubmitRequest request);
}