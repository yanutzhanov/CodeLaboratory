using CodeLaboratory.Enteties;
using System.Threading.Tasks;

namespace CodeLaboratory.Data.Repositories.Abstract
{
    public interface IProjectsRepository : ICRUDRepository<ProjectEntity>
    {
        Task JoinToProject(int projectId, string userIdentityLogin);
        Task Create(ProjectEntity entity, string userIdentityLogin);
    }
}
