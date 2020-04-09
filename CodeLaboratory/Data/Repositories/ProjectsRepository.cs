using CodeLaboratory.Data.Contexts;
using CodeLaboratory.Data.Repositories.Abstract;
using CodeLaboratory.Enteties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLaboratory.Data.Repositories
{
    public class ProjectsRepository : BaseRepository<ProjectEntity>, IProjectsRepository
    {
        private readonly CodeLabDbContext _context;
        public ProjectsRepository(CodeLabDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task JoinToProject(int projectId, string userIdentityLogin)
        {
            ProjectEntity project = _context.Projects.Find(projectId);
            UserEntity user = await _context.Users.FirstOrDefaultAsync(u => u.Login.ToLower() == userIdentityLogin.ToLower());
            _context.UserProjects.Add(new UserProjectEntity { Project = project, User = user });
            await _context.SaveChangesAsync();
        }

        public async Task Create(ProjectEntity entity, string userIdentityLogin)
        {
            UserEntity user = _context.Users.FirstOrDefault(u => u.Login.ToLower() == userIdentityLogin.ToLower());
            entity.Owner = user;
            _context.Projects.Add(entity);
            _context.UserProjects.Add(new UserProjectEntity { Project = entity, User = user });
            await _context.SaveChangesAsync();
        }

        public new async Task<IEnumerable<ProjectEntity>> Get()
        {
            return await _context.Projects.Include(p => p.UserProjects).AsNoTracking().ToListAsync();
        }

        public new async Task<ProjectEntity> GetById(int id)
        {
            return await _context.Projects.Include(p => p.UserProjects).FirstOrDefaultAsync(p => p.Id == id);
        }

    }
}
