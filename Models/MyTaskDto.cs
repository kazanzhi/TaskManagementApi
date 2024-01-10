using System.Text.Json.Serialization;
using TaskManagementApi.Authentication;

namespace TaskManagementApi.Models
{
    public class MyTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDone { get; set; } = true;
    }
}
