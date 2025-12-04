namespace DanskeLearning.Services.ArticleReadStatusService;

public interface IArticleReadStatusService
{
    Task<bool> GetReadStatusAsync(Guid userId, int articleId);
    Task SetReadStatusAsync(Guid userId, int articleId, bool isRead);
}

