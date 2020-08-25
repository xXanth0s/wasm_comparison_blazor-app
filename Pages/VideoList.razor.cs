using BlazorApp.Models;
using BlazorApp.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Pages
{
    public partial class VideoList
    {
        [Inject]
        public IHttpService<Video> HttpService { get; set; }

        private List<Video> Videos;

        protected override async Task OnInitializedAsync()
        {
            Videos = await HttpService.getAll();
        }
    }
}
