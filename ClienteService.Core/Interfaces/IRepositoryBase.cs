using ClienteService.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClienteService.Core.Interfaces
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate = null);

        Task<T> GetById(string id);

        Task Insert(T entity);

        Task Delete(string id);
    }
}
