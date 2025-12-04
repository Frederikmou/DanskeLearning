using Core.DTOs;

namespace Server.Repositories.TestReposi;

public interface ITestRepo
{
    Task<TestDto?> GetTestBySubjectAsync(int subjectId);
    Task<TestSubmitResult> SubmitTestAsync(TestSubmitRequest request);
    Task<bool> GetTestCompletionStatusAsync(Guid userId, int subjectId);
    Task SetTestCompletionStatusAsync(Guid userId, int subjectId, bool passed);
}