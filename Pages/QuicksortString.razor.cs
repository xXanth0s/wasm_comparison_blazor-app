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
    public partial class QuicksortString
    {

        [Inject]
        public IHttpService<string> HttpService { get; set; }

        [Inject]
        public NavigationManager NavigationManager  { get; set; }
        
        [Inject]
        public IHttpService<Result> ResultHttpService { get; set; }

        [Inject]
        public IQuickSortService<string> QuickSortService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public IConfiguration Configuration { get; set; }
        
        [Inject] public IResultService ResultService { get; set; }

        [Parameter]
        public int RunCount { get; set; }  
        
        [Parameter]
        public int Count { get; set; }  
        
        [Parameter]
        public Boolean AutoStart { get; set; }
        
        [Parameter]
        public Boolean IsJs { get; set; }

        private string[] _sortableDataArray;
        private string[] _preview;

        private Boolean _isFinished = false;
        private Boolean _isRunning = false;
        private long _elapsedTime = 0;

        protected override async Task OnInitializedAsync()
        {
            _sortableDataArray = await HttpService.getAllAsArray(Count);
            if (AutoStart)
            {
                if (IsJs)
                {
                    this.SortJS();
                }
                else
                {
                    this.Sort();
                }
            }
        }

        private void Sort()
        {
            _isRunning = true;
            _isFinished = false;
            var watch = Stopwatch.StartNew();

            QuickSortService.sort(ref _sortableDataArray, "ID");
            watch.Stop();
            _elapsedTime = watch.ElapsedMilliseconds;

            _preview = _sortableDataArray.Take(100).ToArray();

            _isRunning = false;
            _isFinished = true;
            
            ResultService.SaveResult(false, AutoStart, _elapsedTime, "string", Count, RunCount);
        }
        private async void SortJS()
        {
            _isRunning = true;
            _isFinished = false;
            var watch = Stopwatch.StartNew();
            
            var result = await JSRuntime.InvokeAsync<string[]>("quickSort",  _sortableDataArray as Object);
            watch.Stop();
            _elapsedTime = watch.ElapsedMilliseconds;

            _preview = result.Take(1000).ToArray();

            _isRunning = false;
            _isFinished = true;

            base.StateHasChanged();

            ResultService.SaveResult(true, AutoStart, _elapsedTime, "string", Count, RunCount);
        }
    }
}
