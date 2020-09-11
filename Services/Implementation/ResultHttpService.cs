using BlazorApp.Models;
using System.Net.Http;

namespace BlazorApp.Services.Implementation
{
    public class ResultHttpService: HttpAbstractService<Result>
    {

        public ResultHttpService(HttpClient http) : base(http, "result")
        {           
        }

    }
}
