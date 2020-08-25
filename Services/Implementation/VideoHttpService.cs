using BlazorApp.Models;
using System.Net.Http;

namespace BlazorApp.Services.Implementation
{
    public class VideoHttpService: HttpAbstractService<Video>
    {

        public VideoHttpService(HttpClient http) : base(http, "video")
        {           
        }

    }
}
