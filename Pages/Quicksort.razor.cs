using BlazorApp.Models;
using BlazorApp.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Pages
{
    public partial class Quicksort
    {

        [Inject]
        public IHttpService<SortableData> HttpService { get; set; }

        [Inject]
        public IQuickSortService<SortableData> QuickSortService { get; set; }

        private SortableData[] SortableDataArray;
        private SortableData[] Preview;

        private Boolean IsFinished = false;
        private Boolean IsRunning = false;
        private long ElapsedTime = 0;

        protected override async Task OnInitializedAsync()
        {
            SortableDataArray = await HttpService.getAllAsArray();
            Console.WriteLine("data loaded");
        }

        private void sort()
        {
            IsRunning = true;
            IsFinished = false;
            Console.WriteLine("Sorting started");
            var watch = Stopwatch.StartNew();

            QuickSortService.sort(ref SortableDataArray, "ID");
            watch.Stop();
            ElapsedTime = watch.ElapsedMilliseconds;

            Console.WriteLine("Sorting finished");
            Preview = SortableDataArray.Take(100).ToArray();

            IsRunning = false;
            IsFinished = true;
        }

    }
}
