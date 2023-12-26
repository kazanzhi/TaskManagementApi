﻿namespace TaskManagementApi.Models
{
    public class MyTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDone { get; set; } = true;
    }
}
