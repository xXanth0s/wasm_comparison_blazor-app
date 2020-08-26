using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using BlazorApp.Services.Interfaces;

namespace BlazorApp.Services.Implementation
{
    public abstract class HttpAbstractService<T>: IHttpService<T>
    {
        private string Endpoind;
        private HttpClient Http;


        public HttpAbstractService(HttpClient http, string endpoind)
        {
            this.Http = http;
            this.Endpoind = endpoind;
        }

        public async Task<List<T>> getAll()
        {
            List<T> result = new List<T>();
            try
            {
                result = await Http.GetFromJsonAsync<List<T>>(Endpoind);
            }
            catch
            {
                Console.WriteLine("Error whild fetching data for ", Endpoind);
            }

            return result;
        }

        public async Task<T[]> getAllAsArray()
        {
            T[] result = new T[0];
            try
            {
                result = await Http.GetFromJsonAsync<T[]>(Endpoind);
            }
            catch
            {
                Console.WriteLine("Error whild fetching data for ", Endpoind);
            }

            return result;
        }

        public async Task<T> getById(string id)
        {
            T result = default(T);
            try
            {
                result = await Http.GetFromJsonAsync<T>(String.Format("{0}/{1}", Endpoind, id));
            }
            catch
            {
                Console.WriteLine("Error whild fetching data for ", Endpoind);
            }

            return result;
        }

        public async Task<Boolean> Delete(string id)
        {
            try {
                await Http.DeleteAsync(String.Format("{0}/{1}", Endpoind, id));
            }
            catch
            {
                Console.WriteLine("Error whild fetching data for ", Endpoind);
                return false;
            }

            return true;
        }

        public async Task<T> Insert<K>(K dataToSave)
        {
            T result = default(T);

            try {
                var response = await Http.PostAsJsonAsync(Endpoind, dataToSave);

                result = await response.Content.ReadAsAsync<T>();
            }
            catch
            {
                Console.WriteLine("Error whild inserting data for ", Endpoind);
            }

            return result;
        }

        public async Task<T> Update(T data, string id)
        {
            T result = default(T);
            try
            {
                var response = await Http.PutAsJsonAsync(String.Format("{0}/{1}", Endpoind, id), data);

                result = await response.Content.ReadAsAsync<T>();
            }
            catch
            {
                Console.WriteLine("Error whild updating data for ", Endpoind);
            }

            return result;
        }

    }
}
