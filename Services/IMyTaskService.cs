using TaskManagementApi.Common;
using TaskManagementApi.Models;

namespace TaskManagementApi.Services
{
    public interface IMyTaskService
    {
        Task<ServiceResponse<List<MyTask>>> GetAllAsync();
        Task<ServiceResponse<MyTask>> GetByIdAsync(int id);
        Task<ServiceResponse<MyTask>> UpdateAsync(MyTask task);
        Task<ServiceResponse<List<MyTask>>> DeleteAsync(int id);
        Task<ServiceResponse<MyTask>> CreateAsync(MyTask task);
    }
}
