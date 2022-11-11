using CSARN.SharedLib.Generics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSARN.SharedLib.Generics.Repositories
{
    public interface IBaseRepository<TEntity,TKey>
        where TEntity : class
        where TKey : struct
    {
        public Task CreateAsync(TEntity entity);
        public Task<TEntity?> GetByIdAsync(TKey id);
        public Task<List<TEntity>> GetAllAsync(int pageNum, int pageSize);
        public Task UpdateAsync(TEntity entity);
        public Task DeleteAsync(TKey id);
    }
}
