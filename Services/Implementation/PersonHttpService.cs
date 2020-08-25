using BlazorApp.Models;
using System.Net.Http;

namespace BlazorApp.Services.Implementation
{
    public class PersonHttpService: HttpAbstractService<Person>
    {

        public PersonHttpService(HttpClient http) : base(http, "person")
        {           
        }

    }
}
