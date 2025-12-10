using System;
using System.Collections.Generic;

namespace Server.Repositories.AdminAssignRepo;

public interface IAdminAssignRepo
{
    Task<bool> AssignSubjectToUser(Guid userId, Guid subjectId);
    Task<bool> AssignSubjectToUsers(int subjectId, List<Guid> userIds);
}