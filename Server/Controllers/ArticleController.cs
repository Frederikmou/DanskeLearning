using Microsoft.AspNetCore.Mvc;
using Server.Repositories.ArticleRepository;
using Server.Repositories.ArticleReadStatusRepository;

namespace Server.Controllers;

[ApiController]
[Route("api/articles")]

public class ArticleController : ControllerBase
{
 
    private readonly IArticlesRepo  _articlesRepo;
    private readonly IArticleReadStatusRepo _readStatusRepo;

    public ArticleController(IArticlesRepo articlesRepo, IArticleReadStatusRepo readStatusRepo)
    {
        _articlesRepo = articlesRepo;
        _readStatusRepo = readStatusRepo;
    }

    [HttpGet("subject/{subjectId}")]
    public async Task<IActionResult> GetArticlesByIdAsync(int subjectId)
    {
        var articles = await _articlesRepo.GetArticlesByIdAsync(subjectId);
        return Ok(articles);
    }

    [HttpGet("single/{articleId}")]
    public async Task<IActionResult> GetSingleArticleAsync(int articleId)
    {
        var article = await  _articlesRepo.GetSingleArticleAsync(articleId);
        return Ok(article);
    }

    [HttpGet("readstatus/{userId}/{articleId}")]
    public async Task<IActionResult> GetReadStatusAsync(Guid userId, int articleId)
    {
        var isRead = await _readStatusRepo.GetReadStatusAsync(userId, articleId);
        return Ok(isRead);
    }

    [HttpPost("readstatus/{userId}/{articleId}")]
    public async Task<IActionResult> SetReadStatusAsync(Guid userId, int articleId, [FromBody] bool isRead)
    {
        await _readStatusRepo.SetReadStatusAsync(userId, articleId, isRead);
        return Ok();
    }

}