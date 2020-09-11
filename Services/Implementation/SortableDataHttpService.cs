using BlazorApp.Models;
using System.Net.Http;

namespace BlazorApp.Services.Implementation
{
    public class SortableDataHttpService: HttpAbstractService<string>
    {

        public SortableDataHttpService(HttpClient http) : base(http, "sortable")
        {           
        }

    }
}
