using BlazorApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;

namespace BlazorApp.Shared.Components
{
    public partial class VideoTile
    {

        [Parameter]
        public Video VideoData { get; set; }

        [Inject]
        public IConfiguration Configuration{ get; set; }

        public string ThumbNailUrl {
            get
            {
                if (VideoData == null)
                {
                    return "";
                }
                Console.WriteLine(VideoData.id);
                return String.Format("{0}video/thumbnail/{1}", Configuration["API_Url"], VideoData.id);
            }
        }

        public string VideoUrl {
            get
            {
                if (VideoData == null)
                {
                    return "";
                }

                return String.Format("/videodetail/{0}", VideoData.id);
            }
        }

    }
}
