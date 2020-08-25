using BlazorApp.Models;
using BlazorApp.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace BlazorApp.Pages
{
    public partial class VideoDetail
    {
        [Inject]
        public IHttpService<Video> HttpService { get; set; }

        [Inject]
        public IConfiguration Configuration { get; set; }

        [Parameter]
        public string ID { get; set; }

        private string VideoUrl
        {
            get
            {
                if(VideoData == null)
                {
                    return "";
                }

                return String.Format("{0}video/data/{1}", Configuration["API_Url"], ID);
            }
        }

        private Video VideoData;

        protected override async Task OnInitializedAsync()
        {
            VideoData = await HttpService.getById(ID);
        }
    }
}
