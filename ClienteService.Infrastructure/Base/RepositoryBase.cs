using ClienteService.Core.Base;
using ClienteService.Core.Interfaces;
using ClienteService.Core.Options;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClienteService.Infrastructure.Base
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        #region CONSTANTS
        private readonly FirebaseClient _client;
        private readonly ConnectionOptions _connectionOptions;
        private readonly string _nameEntity;
        #endregion

        #region CONSTRUCTOR
        public RepositoryBase(ConnectionOptions connectionOptions)
        {
            _connectionOptions = connectionOptions;
            _client = new FirebaseClient(_connectionOptions.FirebaseConnection);
            _nameEntity = typeof(T).Name;
        }
        #endregion

        #region METHODS
        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            var response = await _client.Child(_nameEntity).OnceAsync<T>();
            var listEnties = response.Select(x => x.Object);
            return listEnties;
        }

        public async Task<T> GetById(string id)
        {
            var response = await _client.Child(_nameEntity).OrderByKey().StartAt(id).LimitToFirst(1).OnceAsync<T>();
            var entity = response.Select(x => x.Object).FirstOrDefault();
            return entity;
        }

        public async Task Insert(T entity)
        {
            var id = Guid.NewGuid().ToString();
            entity.Id = id;
            await _client.Child(_nameEntity).Child(id).PutAsync(entity);
        }

        public async Task Delete(string id)
        {
            await _client.Child(_nameEntity).Child(id).DeleteAsync();
        }
        #endregion
    }
}
