using CodeLaboratory.Enteties;

namespace CodeLaboratory.Data.Repositories.Abstract
{
    public interface IProjectsRepository : ICRUDRepository<ProjectEntity>
    {
        void JoinToProject(int projectId, string userIdentityLogin);
        void Create(ProjectEntity entity, string userIdentityLogin);
    }
}
