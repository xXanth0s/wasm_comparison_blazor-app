using BlazorApp.Models;
using System.Net.Http;

namespace BlazorApp.Services.Implementation
{
    public class SortableNumberHttpService: HttpAbstractService<int>
    {

        public SortableNumberHttpService(HttpClient http) : base(http, "sortable/numbers")
        {           
        }

    }
}
