using BlazorApp.Services.Interfaces;
using BlazorApp.Models;
using BlazorApp.Services.Implementation;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["API_Url"]) });

            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton<IHttpService<ToDoTask>, ToDoTaskHttpService>();
            builder.Services.AddSingleton<IHttpService<Person>, PersonHttpService>();
            builder.Services.AddSingleton<IHttpService<Video>, VideoHttpService>();
            builder.Services.AddSingleton<IHttpService<string>, SortableDataHttpService>();
            builder.Services.AddSingleton<IHttpService<int>, SortableNumberHttpService>();
            builder.Services.AddSingleton<IHttpService<Result>, ResultHttpService>();
            builder.Services.AddSingleton<IQuickSortService<Person>, QuickSortService<Person>>();
            builder.Services.AddSingleton<IQuickSortService<string>, QuickSortServiceDetailed>();
            builder.Services.AddSingleton<IQuickSortService<int>, QuickSortServiceNumber>();

            await builder.Build().RunAsync();
        }
    }
}
