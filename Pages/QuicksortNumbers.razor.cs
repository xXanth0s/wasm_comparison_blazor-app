using BlazorApp.Models;
using BlazorApp.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Pages
{
    public partial class QuicksortNumbers
    {
        [Inject] public IHttpService<int> HttpService { get; set; }

        [Inject] public IHttpService<Result> ResultHttpService { get; set; }

        [Inject] public IQuickSortService<int> QuickSortService { get; set; }

        [Inject] public NavigationManager NavigationManager { get; set; }

        [Inject] public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public IConfiguration Configuration { get; set; }

        [Parameter] public int RunCount { get; set; }

        [Parameter] public int Count { get; set; }

        [Parameter] public Boolean AutoStart { get; set; }

        [Parameter] public Boolean IsJs { get; set; }

        private int[] SortableDataArray;
        private int[] Preview;

        private Boolean IsFinished = false;
        private Boolean IsRunning = false;
        private long ElapsedTime = 0;

        private readonly int[] elementCountCollection = {100000, 50000, 20000, 10000, 5000, 2000, 1000, 500};

        protected override async Task OnInitializedAsync()
        {
            SortableDataArray = await HttpService.getAllAsArray(Count);
            Console.WriteLine("data loaded");
            if (AutoStart)
            {
                if (IsJs)
                {
                    this.sortJS();
                }
                else
                {
                    this.sort();
                }
            }
        }

        private void sort()
        {
            IsRunning = true;
            IsFinished = false;
            Console.WriteLine("Sorting QuicksortNumbers started");
            var watch = Stopwatch.StartNew();

            QuickSortService.sort(ref SortableDataArray, "ID");
            watch.Stop();
            ElapsedTime = watch.ElapsedMilliseconds;

            Console.WriteLine("Sorting finished");
            Preview = SortableDataArray.Take(100).ToArray();

            IsRunning = false;
            IsFinished = true;

            saveReuslt(false);
        }

        private async void sortJS()
        {
            IsRunning = true;
            IsFinished = false;
            Console.WriteLine("Sorting startessd");
            var watch = Stopwatch.StartNew();

            var result = await JSRuntime.InvokeAsync<int[]>("quickSort", SortableDataArray);
            watch.Stop();
            ElapsedTime = watch.ElapsedMilliseconds;

            Console.WriteLine("Sorting finished");
            Preview = result.Take(100).ToArray();

            IsRunning = false;
            IsFinished = true;

            base.StateHasChanged();

            saveReuslt(true);
        }

        private void saveReuslt(Boolean isJs)
        {
            var framework = isJs ? "blazor_with_js" : "blazor";

            var result = new Result
            {
                count = Count,
                framework = framework,
                time = ElapsedTime,
                sortType = "number",
                browser = "safari_mac"
            };
            this.ResultHttpService.Insert(result);

            if (!AutoStart)
            {
                return;
            }

            int maxRunCount = Int16.Parse(Configuration["SORT_ITERATIONS"]);

            if (RunCount < maxRunCount) {
                Console.WriteLine("Redirecting");
                var url = String.Format("/quicksortnumbers/{0}/{1}/{2}/{3}", Count, true, ++RunCount, isJs);

                NavigationManager.NavigateTo(url);

                JSRuntime.InvokeAsync<string>("reloadPage", "");
            }
            else
            {
                var indexForNextElementCount = Array.IndexOf(elementCountCollection, Count) + 1;

                if (indexForNextElementCount < elementCountCollection.Length)
                {
                    var nextCount = elementCountCollection[indexForNextElementCount];
                    var url = String.Format("/quicksortnumbers/{0}/{1}/0/{2}", nextCount, true, isJs);

                    NavigationManager.NavigateTo(url);

                    JSRuntime.InvokeAsync<string>("reloadPage", "");
                    
                }
                else if(isJs)
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