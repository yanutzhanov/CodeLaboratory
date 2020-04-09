using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeLaboratory.Data.Contexts;
using CodeLaboratory.Enteties.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CodeLaboratory.Data.Repositories.Abstract
{
    public class BaseRepository<TEntity> : ICRUDRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly CodeLabDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(CodeLabDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<TEntity>();
        }
        public async Task Create(TEntity entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Update(TEntity entity)
        {
            _context.Update(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
