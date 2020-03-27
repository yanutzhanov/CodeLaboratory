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
        public void Create(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChangesAsync();
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChangesAsync();
        }

        public IEnumerable<TEntity> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChangesAsync();
        }
    }
}
