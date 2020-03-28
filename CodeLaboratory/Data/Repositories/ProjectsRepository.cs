using CodeLaboratory.Data.Contexts;
using CodeLaboratory.Data.Repositories.Abstract;
using CodeLaboratory.Enteties;
using System;
using System.Linq;

namespace CodeLaboratory.Data.Repositories
{
    public class ProjectsRepository : BaseRepository<ProjectEntity>, IProjectsRepository
    {
        private readonly CodeLabDbContext _context;
        public ProjectsRepository(CodeLabDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void JoinToProject(int projectId, string userIdentityLogin)
        {
            ProjectEntity project = _context.Projects.Find(projectId);
            UserEntity user = _context.Users.FirstOrDefault(u => u.Login.ToLower() == userIdentityLogin.ToLower());
            _context.UserProjects.Add(new UserProjectEntity { Project = project, User = user });
            _context.SaveChangesAsync();
        }

        public void Create(ProjectEntity entity, string userIdentityLogin)
        {
            UserEntity user = _context.Users.FirstOrDefault(u => u.Login.ToLower() == userIdentityLogin.ToLower());
            entity.Owner = user;
            _context.Projects.Add(entity);
            _context.UserProjects.Add(new UserProjectEntity { Project = entity, User = user });
            _context.SaveChanges();
        }

    }
}
