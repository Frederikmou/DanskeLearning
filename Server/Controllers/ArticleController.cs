using Microsoft.AspNetCore.Mvc;
using Server.Repositories.ArticleRepository;

namespace Server.Controllers;

[ApiController]
[Route("api/articles")]

public class ArticleController : ControllerBase
{
 
 private readonly IArticlesRepo  _articlesRepo;

 public ArticleController(IArticlesRepo articlesRepo)
 {
  _articlesRepo = articlesRepo;
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

}