using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CentralService.Api.ContentDelivery.Interfaces.Repositories;

namespace CentralService.Api.ContentDelivery.Repository.Repositories
{
    public abstract class Repository<TType> : IRepository<TType> where TType : class
    {
        protected Context _Database;

        public Repository() => _Database = new Context();

        public async Task<TType> Get(Expression<Func<TType, bool>> Predicate) => await _Database.Set<TType>().FirstOrDefaultAsync(Predicate);

        public async Task<List<TType>> Get() => await _Database.Set<TType>().ToListAsync();

        public async Task<List<TType>> Find(Expression<Func<TType, bool>> Predicate) => await _Database.Set<TType>().Where(Predicate).ToListAsync();

        public async Task Add(TType Entity) => await _Database.Set<TType>().AddAsync(Entity);

        public void Update(TType Entity) => _Database.Set<TType>().Update(Entity);

        public void Remove(TType Entity) => _Database.Set<TType>().Remove(Entity);

        public async ValueTask DisposeAsync()
        {
            await _Database.SaveChangesAsync();
            await _Database.DisposeAsync();
        }
    }
}
