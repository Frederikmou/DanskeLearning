using Core.Models;

namespace Server.Repositories.ArticleReadStatusRepository;

public interface IArticleReadStatusRepo
{
    Task<bool> GetReadStatusAsync(Guid userId, int articleId);
    Task SetReadStatusAsync(Guid userId, int articleId, bool isRead);
}

