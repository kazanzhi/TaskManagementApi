using TaskManagementApi.Common;
using TaskManagementApi.Models;

namespace TaskManagementApi.Repositories
{
    public interface IRepository<T>
    {
        Task<ServiceResponse<List<T>>> GetAllAsync();
        Task<ServiceResponse<T>> GetByIdAsync(int id);
        Task<ServiceResponse<T>> UpdateAsync(T task);
        Task<ServiceResponse<List<T>>> DeleteAsync(int id);
        Task<ServiceResponse<T>> CreateAsync(T task);
    }
}
