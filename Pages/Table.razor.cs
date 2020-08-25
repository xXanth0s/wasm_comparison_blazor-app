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

        [Inject]
        public IQuickSortService<Person> QuickSortService { get; set; }

        private Person[] persons;

        protected override async Task OnInitializedAsync()
        {
            persons = (await HttpService.getAll()).ToArray();
        }

        private void sort(string key)
        {
            QuickSortService.sort(ref persons, key);
        }
    }
}
