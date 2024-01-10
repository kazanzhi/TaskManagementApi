using Microsoft.AspNetCore.Hosting.Server;
using TaskManagementApi.Common;
using TaskManagementApi.Models;

namespace TaskManagementApi.Services.MyTaskServices
{
    public interface IMyTaskService
    {
        Task<ServiceResponse<List<MyTask>>> GetAllAsync(string userId);
        Task<ServiceResponse<MyTask>> GetByIdAsync(int id, string userId);
        Task<ServiceResponse<MyTask>> UpdateAsync(MyTask task, string userId);
        Task<ServiceResponse<List<MyTask>>> DeleteAsync(int id, string userId);
        Task<ServiceResponse<MyTask>> CreateAsync(MyTaskDto task, string userId);
    }
}
