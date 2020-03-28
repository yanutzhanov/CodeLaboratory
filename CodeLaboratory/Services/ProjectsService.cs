using CodeLaboratory.Data.Repositories.Abstract;
using CodeLaboratory.Domain;
using CodeLaboratory.Enteties;
using CodeLaboratory.Services.Abstract;
using Mapster;
using System;
using System.Collections.Generic;

namespace CodeLaboratory.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository _projectRepository;

        public ProjectsService(IProjectsRepository projectRepository)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        public void JoinToProject(int projectId, string userIdentityLogin)
        {
            if (string.IsNullOrEmpty(userIdentityLogin)) throw new ArgumentException(nameof(userIdentityLogin));

            _projectRepository.JoinToProject(projectId, userIdentityLogin);
        }

        public IEnumerable<ProjectEntity> GetAll()
        {
            return _projectRepository.Get();
        }

        public ProjectEntity Get(int id)
        {
            return _projectRepository.GetById(id);
        }

        public void Delete(Project project)
        {
            if (project is null) throw new ArgumentNullException(nameof(project));

            _projectRepository.Delete(project.Adapt<ProjectEntity>());
        }

        public void Update(Project project)
        {
            if (project is null) throw new ArgumentNullException(nameof(project));

            _projectRepository.Update(project.Adapt<ProjectEntity>());
        }

        public void Create(Project project, string userIdentityLogin)
        {
            if (project is null) throw new ArgumentNullException(nameof(project));
            if (string.IsNullOrEmpty(userIdentityLogin)) throw new ArgumentException("message", nameof(userIdentityLogin));

            _projectRepository.Create(project.Adapt<ProjectEntity>(), userIdentityLogin);
        }

        public void Create(Project project)
        {
            if (project is null) throw new ArgumentNullException(nameof(project));

            _projectRepository.Create(project.Adapt<ProjectEntity>());
        }
    }
}
