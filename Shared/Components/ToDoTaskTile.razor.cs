using BlazorApp.Models;
using BlazorApp.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Shared.Components
{
    public partial class ToDoTaskTile
    {
        [Inject]
        public IHttpService<ToDoTask> HttpService { get; set; }

        [Parameter]
        public ToDoTask ToDoTask { get; set; }

        [Parameter]
        public EventCallback<ToDoTask> OnDelete { get; set; }

        private async void toggleTask()
        {
            ToDoTask.completed = !ToDoTask.completed;
            await HttpService.Update(ToDoTask, ToDoTask._id);
         

            base.StateHasChanged();
        }

        private void deleteTask()
        {
            OnDelete.InvokeAsync(ToDoTask);
        }
    }
}
