using BlazorApp.Models;
using BlazorApp.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorApp.Pages
{
    public partial class Table
    {
        [Inject]
        public IHttpService<Person> HttpService { get; set; }

        private Person[] persons;

        protected override async Task OnInitializedAsync()
        {
            persons = await HttpService.getAllAsArray();
        }
    }
}
