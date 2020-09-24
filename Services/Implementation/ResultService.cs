using BlazorApp.Models;
using BlazorApp.Services.Interfaces;
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;

namespace BlazorApp.Services.Implementation
{
    public class ResultService : IResultService
    {
        private IHttpService<Result> ResultHttpService;

        private IJSRuntime JSRuntime;

        private NavigationManager NavigationManager;

        private IConfiguration Configuration;

        public ResultService (IHttpService<Result> resultHttpService, IJSRuntime jSRuntime, NavigationManager navigationManager,
            IConfiguration configuration)
        {
            this.ResultHttpService = resultHttpService;
            this.JSRuntime = jSRuntime;
            this.NavigationManager = navigationManager;
            this.Configuration = configuration;
        }


        private readonly int[] elementCountCollection = {100000, 50000, 20000, 10000, 5000, 2000, 1000, 500};

        public void SaveResult(Boolean isJs, Boolean autoStart, long elapsedTime, string sortType, int count,
            int runCount)
        {
            var framework = isJs ? "blazor_with_js" : "blazor";

            var result = new Result
            {
                count = count,
                framework = framework,
                time = elapsedTime,
                sortType = sortType,
                browser = "chrome"
            };

            this.ResultHttpService.Insert(result);

            if (!autoStart)
            {
                return;
            }

            int maxRunCount = Int16.Parse(Configuration["SORT_ITERATIONS"]);

            if (runCount < maxRunCount)
            {
                var url = String.Format("/quicksortnumbers/{0}/{1}/{2}/{3}", count, true, ++runCount, isJs);

                NavigationManager.NavigateTo(url);

                JSRuntime.InvokeAsync<string>("reloadPage", "");
            }
            else
            {
                var indexForNextElementCount = Array.IndexOf(elementCountCollection, count) + 1;

                if (indexForNextElementCount < elementCountCollection.Length)
                {
                    var nextCount = elementCountCollection[indexForNextElementCount];
                    var url = String.Format("/quicksortnumbers/{0}/{1}/0/{2}", nextCount, true, isJs);

                    NavigationManager.NavigateTo(url);

                    JSRuntime.InvokeAsync<string>("reloadPage", "");
                }
                else if (isJs)
                {
                    var nextCount = elementCountCollection[0];
                    var url = String.Format("/quicksortnumbers/{0}/true/0/false", nextCount);

                    NavigationManager.NavigateTo(url);

                    JSRuntime.InvokeAsync<string>("reloadPage", "");
                }
                else
                {
                    var nextCount = elementCountCollection[0];
                    var url = String.Format("/quicksortstring/{0}/true/0/false", nextCount);

                    NavigationManager.NavigateTo(url);

                    JSRuntime.InvokeAsync<string>("reloadPage", "");
                }
            }
        }
    }
}