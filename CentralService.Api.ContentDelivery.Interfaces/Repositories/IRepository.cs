using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CentralService.Api.ContentDelivery.Interfaces.Repositories
{
    public interface IRepository<TType> : IAsyncDisposable where TType : class
    {
        Task<TType> Get(Expression<Func<TType, bool>> Predicate);
        Task<List<TType>> Get();
        Task<List<TType>> Find(Expression<Func<TType, bool>> Predicate);
        Task Add(TType Entity);
        void Update(TType Entity);
        void Remove(TType Entity);

    }
}
