using Core.Models;
namespace Server.Repositories.ArticleRepository;

public interface IArticlesRepo 
{
    public Task<List<Articles>> GetArticlesByIdAsync(int subjectId);
    
    public Task<List<Articles>> GetSingleArticleAsync(int articleId);
}