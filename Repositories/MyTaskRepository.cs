using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Common;
using TaskManagementApi.Data;
using TaskManagementApi.Models;

namespace TaskManagementApi.Repositories
{
    public class MyTaskRepository : IRepository<MyTask>
    {
        private readonly DataContext context;

        public MyTaskRepository(DataContext context)
        {
            this.context = context;
        }
        public async Task<ServiceResponse<MyTask>> CreateAsync(MyTask newTask)
        {
            try
            {
                var task = new MyTask();
                task.Title = newTask.Title;
                task.Description = newTask.Description;
                task.IsDone = newTask.IsDone;

                context.Tasks.Add(task);

                await context.SaveChangesAsync();

                return new ServiceResponse<MyTask>(task, "Task retrived succesfully", true);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<MyTask>(null, $"Error retriving task: {ex.Message}", false);
            }
        }

        public async Task<ServiceResponse<List<MyTask>>> DeleteAsync(int id)
        {
            try
            {
                var task = await context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
                if(task != null)
                {
                    context.Tasks.Remove(task);
                    await context.SaveChangesAsync();
                    var tasks = await context.Tasks.ToListAsync();
                    return new ServiceResponse<List<MyTask>>(tasks, "Tasks retrived succesfully", true);
                }
                return new ServiceResponse<List<MyTask>>(null, "Task not found", false);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<MyTask>>(null, $"Error retriving tasks: {ex.Message}", false);
            }
        }

        public async Task<ServiceResponse<List<MyTask>>> GetAllAsync()
        {
            try
            {
                var tasks = await context.Tasks.ToListAsync();
                return new ServiceResponse<List<MyTask>>(tasks, "Tasks retrieved successfully", true);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<MyTask>>(null, $"Error retrieving tasks: {ex.Message}", false);
            }
            
        }

        public async Task<ServiceResponse<MyTask>> GetByIdAsync(int id)
        {
            try
            {
                var task = await context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
                if (task is null)
                    return new ServiceResponse<MyTask>(null, "Task not found", false);
                return new ServiceResponse<MyTask>(task, "Task retrived succesfully", true);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<MyTask>(null,$"Error retriving task: {ex.Message}", false);
            }
        }

        public async Task<ServiceResponse<MyTask>> UpdateAsync(MyTask updateTask)
        {
            try
            {
                var task = await context.Tasks.FirstOrDefaultAsync(x => x.Id == updateTask.Id);

                task.Title = updateTask.Title;
                task.Description = updateTask.Description;
                task.IsDone = updateTask.IsDone;

                await context.SaveChangesAsync();

                return new ServiceResponse<MyTask>(task, "Task retrived succesfully", true);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<MyTask>(null, $"Error retriving task", false);
            }
        }
    }
}
