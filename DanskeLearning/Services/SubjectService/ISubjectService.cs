using Core.Models;
namespace DanskeLearning.Services.SubjectService;
public interface ISubjectService
{
    Task<List<Articles>> GetAllArticlesAsync();
}