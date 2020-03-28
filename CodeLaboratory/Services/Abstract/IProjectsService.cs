using CodeLaboratory.Domain;
using CodeLaboratory.Enteties;
using System.Collections.Generic;

namespace CodeLaboratory.Services.Abstract
{
    public interface IProjectsService
    {
        void JoinToProject(int projectId, string userIdentityLogin);
        IEnumerable<ProjectEntity> GetAll();
        ProjectEntity Get(int id);
        void Delete(Project project);
        void Update(Project project);
        void Create(Project project);
        void Create(Project project, string userIdentityLogin);
    }
}
