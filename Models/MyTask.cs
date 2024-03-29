﻿using System.Text.Json.Serialization;
using TaskManagementApi.Authentication;

namespace TaskManagementApi.Models
{
    public class MyTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDone { get; set; } = true;

        [JsonIgnore]
        public ApplicationUser? User { get; set; }

        [JsonIgnore]
        public string? UserId { get; set; }
    }
}
