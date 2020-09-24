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

        [Inject] public IQuickSortService<int> QuickSortService { get; set; }

        [Inject] public IResultService ResultService { get; set; }

        [Inject] public IJSRuntime JSRuntime { get; set; }

        [Parameter] public int RunCount { get; set; }

        [Parameter] public int Count { get; set; }

        [Parameter] public Boolean AutoStart { get; set; }

        [Parameter] public Boolean IsJs { get; set; }

        private int[] SortableDataArray;
        private int[] Preview;

        private Boolean IsFinished = false;
        private Boolean IsRunning = false;
        private long ElapsedTime = 0;


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

            ResultService.SaveResult(false, AutoStart, ElapsedTime, "number", Count, RunCount);
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

            ResultService.SaveResult(true, AutoStart, ElapsedTime, "number", Count, RunCount);
        }
    }
}