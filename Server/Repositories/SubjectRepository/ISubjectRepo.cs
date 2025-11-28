using Core.Models;

namespace Server.Repositories.SubjectRepository;

public interface ISubjectRepo
{
    public Task<List<Articles>> GetAllArticleByIdAsync();
}