using Core.Models;

namespace DanskeLearning.Services.ArticleService;

public interface IArticlesService
{
    Task<List<Articles>> GetArticlesByIdAsync(int  subjectId);
    
    Task<List<Articles>> GetSingleArticlesAsync(int articleId);
}