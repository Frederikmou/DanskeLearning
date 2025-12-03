namespace Core.Models;

public class ArticleReadStatus
{
    public Guid UserId { get; set; }
    public int ArticleId { get; set; }
    public bool IsRead { get; set; }
}

