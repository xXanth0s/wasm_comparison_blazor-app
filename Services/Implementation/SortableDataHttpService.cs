using BlazorApp.Models;
using System.Net.Http;

namespace BlazorApp.Services.Implementation
{
    public class SortableDataHttpService: HttpAbstractService<SortableData>
    {

        public SortableDataHttpService(HttpClient http) : base(http, "sortable")
        {           
        }

    }
}
