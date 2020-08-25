using BlazorApp.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Shared.Components
{
    public partial class PersonTableRow
    {
        [Parameter]
        public Person Person { get; set; }
    }
}
