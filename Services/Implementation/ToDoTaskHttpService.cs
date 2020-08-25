using BlazorApp.Models;
using System.Net.Http;

namespace BlazorApp.Services.Implementation
{
    public class ToDoTaskHttpService : HttpAbstractService<ToDoTask>
    {

        public ToDoTaskHttpService(HttpClient http) : base(http, "task")
        {
        }

    }
}
