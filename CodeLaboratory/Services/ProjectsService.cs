using CodeLaboratory.Data.Repositories.Abstract;
using CodeLaboratory.Domain;
using CodeLaboratory.Enteties;
using CodeLaboratory.Services.Abstract;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLaboratory.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository _projectRepository;

        public ProjectsService(IProjectsRepository projectRepository)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        public async Task JoinToProject(int projectId, string userIdentityLogin)
        {
            if (string.IsNullOrEmpty(userIdentityLogin)) throw new ArgumentException(nameof(userIdentityLogin));

            await _projectRepository.JoinToProject(projectId, userIdentityLogin);
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            var projectEnitites =  await _projectRepository.Get();
            List<Project> projects = new List<Project>();
            foreach (var projectEntity in projectEnitites)
            {
                var project = projectEntity.Adapt<Project>();
                project.Users = projectEntity.UserProjects.Select(u => u.Adapt<UserProject>());
                projects.Add(project);
            }
            return projects;
        }

        public async Task<ProjectEntity> Get(int id)
        {
            return await _projectRepository.GetById(id);
        }

        public async Task Delete(Project project)
        {
            if (project is null) throw new ArgumentNullException(nameof(project));

            await _projectRepository.Delete(project.Adapt<ProjectEntity>());
        }

        public async Task Update(Project project)
        {
            if (project is null) throw new ArgumentNullException(nameof(project));

            await _projectRepository.Update(project.Adapt<ProjectEntity>());
        }

        public async Task Create(Project project, string userIdentityLogin)
        {
            if (project is null) throw new ArgumentNullException(nameof(project));
            if (string.IsNullOrEmpty(userIdentityLogin)) throw new ArgumentException("message", nameof(userIdentityLogin));

            await _projectRepository.Create(project.Adapt<ProjectEntity>(), userIdentityLogin);
        }

        public async Task Create(Project project)
        {
            if (project is null) throw new ArgumentNullException(nameof(project));

            await _projectRepository.Create(project.Adapt<ProjectEntity>());
        }
    }
}
