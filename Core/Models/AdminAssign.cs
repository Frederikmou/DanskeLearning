namespace Core.Models;

public class AdminAssign
{
    public Guid UserId { get; set; }
    public int SubjectId { get; set; }
    public DateTime AssignedDate { get; set; }
}