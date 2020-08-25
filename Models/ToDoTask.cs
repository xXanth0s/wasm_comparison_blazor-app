using System;

namespace BlazorApp.Models
{
    public class ToDoTask
    {
        public string _id { get; set; }
        public Boolean completed { get; set; }
        public string dateAdded { get; set; }
        public string dateUpdated { get; set; }
        public string description { get; set; }
    }
}
