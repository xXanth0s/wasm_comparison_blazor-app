using BlazorApp.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Shared.Components
{
    public partial class PersonTable
    {
        
        [Parameter]
        public Person[] Persons { get; set; }
        
        
        [Parameter]
        public EventCallback<string> SortedCallback { get; set; }

        public void Sort(string columnClicked)
        {
            this.SortedCallback.InvokeAsync(columnClicked);
        }
    }
}