using System.Collections.Generic;
using CodeLaboratory.Enteties.Abstract;

namespace CodeLaboratory.Data.Repositories.Abstract
{
    public interface ICRUDRepository<TEntity> where TEntity : BaseEntity
    {
        void Create(TEntity entity);
        void Delete(TEntity entity);
        IEnumerable<TEntity> Get();
        TEntity GetById(int id);
        void Update(TEntity entity);
    }
}
