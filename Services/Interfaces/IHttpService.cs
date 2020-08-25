using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Services.Interfaces
{
    public interface IHttpService<T>
    {
        public Task<List<T>> getAll();

        public Task<T> getById(string id);

        public Task<Boolean> Delete(string id);

        public Task<T> Insert<K>(K dataToSave);

        public Task<T> Update(T data, string id);
    }
}
