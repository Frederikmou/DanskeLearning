using Core.DTOs;

namespace Server.Repositories.TestReposi;

public interface ITestRepo
{
    Task<TestDto?> GetTestBySubjectAsync(int subjectId);
    Task<TestSubmitResult> SubmitTestAsync(TestSubmitRequest request);
}