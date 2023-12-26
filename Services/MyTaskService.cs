using TaskManagementApi.Common;
using TaskManagementApi.Models;
using TaskManagementApi.Repositories;

namespace TaskManagementApi.Services
{
    public class MyTaskService : IMyTaskService
    {
        private readonly IRepository<MyTask> repository;
        public MyTaskService(IRepository<MyTask> repository)
        {
            this.repository = repository;
        }
        public async Task<ServiceResponse<MyTask>> CreateAsync(MyTask task)
        {
            return await repository.CreateAsync(task);
        }

        public async Task<ServiceResponse<List<MyTask>>> DeleteAsync(int id)
        {
            return await repository.DeleteAsync(id);
        }

        public async Task<ServiceResponse<List<MyTask>>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<ServiceResponse<MyTask>> GetByIdAsync(int id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task<ServiceResponse<MyTask>> UpdateAsync(MyTask task)
        {
            return await repository.UpdateAsync(task);
        }
    }
}
