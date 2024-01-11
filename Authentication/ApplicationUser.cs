using Microsoft.AspNetCore.Identity;
using TaskManagementApi.Models;

namespace TaskManagementApi.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public List<MyTask> MyTask { get; set; }
    }
}
