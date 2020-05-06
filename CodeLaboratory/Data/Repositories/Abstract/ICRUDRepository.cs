using System.Collections.Generic;
using System.Threading.Tasks;
using CodeLaboratory.Enteties.Abstract;

namespace CodeLaboratory.Data.Repositories.Abstract
{
    public interface ICRUDRepository<TEntity> where TEntity : BaseEntity
    {
        Task Create(TEntity entity);
        Task Delete(TEntity entity);
        Task<IEnumerable<TEntity>> Get();
        Task<TEntity> GetById(int id);
        Task Update(TEntity entity);
    }
}
