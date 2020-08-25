using BlazorApp.Models;
using BlazorApp.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Pages
{
    public partial class ToDo
    {
        [Inject]
        public IHttpService<ToDoTask> HttpService { get; set; }

        private List<ToDoTask> tasks;

        private string newTaskText { get; set; }

        private Boolean isAddingTask = false;

        protected override async Task OnInitializedAsync()
        {
            tasks = await HttpService.getAll();
        }

        private async void deleteTask(ToDoTask taskToDelete)
        {
            var success = await HttpService.Delete(taskToDelete._id);

            if(success)
            {
                tasks = tasks.Where(task => task._id != taskToDelete._id).ToList();

                base.StateHasChanged();
            }
        }

        private void openAddTask()
        {
            this.isAddingTask = true;
        }


        private async void addTask()
        {
            if (newTaskText?.Length > 0)
            {
                var taskToSave = new { description = newTaskText };

                var newTask = await HttpService.Insert(taskToSave);

                if (newTask != null)
                {
                    this.isAddingTask = false;
                    newTaskText = "";
                    tasks.Add(newTask);
                }
                base.StateHasChanged();
            }

        }
    }
}
