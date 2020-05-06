using CodeLaboratory.Domain;
using CodeLaboratory.Enteties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeLaboratory.Services.Abstract
{
    public interface IProjectsService
    {
        Task JoinToProject(int projectId, string userIdentityLogin);
        Task<IEnumerable<Project>> GetAll();
        Task<Project> Get(int id);
        Task Delete(Project project);
        Task Update(Project project);
        Task Create(Project project);
        Task Create(Project project, string userIdentityLogin);
    }
}
