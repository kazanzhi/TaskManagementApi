using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Authentication;
using TaskManagementApi.Common;
using TaskManagementApi.Data;
using TaskManagementApi.Models;

namespace TaskManagementApi.Services.MyTaskServices
{
    public class MyTaskService : IMyTaskService
    {
        private readonly DataContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public MyTaskService(DataContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<ServiceResponse<MyTask>> CreateAsync(MyTaskDto task, string userId)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId);

                var newTask = new MyTask
                {
                    Title = task.Title,
                    Description = task.Description,
                    IsDone = task.IsDone
                };

                newTask.UserId = userId;

                context.Tasks.Add(newTask);

                await context.SaveChangesAsync();

                return new ServiceResponse<MyTask>(newTask, "Task retrived succesfully", true);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<MyTask>(null, $"Error retriving task: {ex.Message}", false);
            }
        }

        public async Task<ServiceResponse<List<MyTask>>> DeleteAsync(int id, string userId)
        {
            try
            {
                var task = await context.Tasks
                    .Where(x => x.UserId == userId)
                    .FirstOrDefaultAsync(t => t.Id == id);
                if (task != null)
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

        public async Task<ServiceResponse<List<MyTask>>> GetAllAsync(string userId)
        {
            try
            {
                var tasks = await context.Tasks
                    .Where(x => x.UserId == userId)
                    .OrderBy(t => t.Title)
                    .ToListAsync();
                if (tasks.Count == 0)
                    return new ServiceResponse<List<MyTask>>(null, "No tasks at the moment", true);

                return new ServiceResponse<List<MyTask>>(tasks, "Tasks retrieved successfully", true);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<MyTask>>(null, $"Error retrieving tasks: {ex.Message}", false);
            }
        }

        public async Task<ServiceResponse<MyTask>> GetByIdAsync(int id, string userId)
        {
            try
            {
                var task = await context.Tasks
                    .Where(x => x.UserId == userId)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (task is null)
                    return new ServiceResponse<MyTask>(null, "Task not found", false);
                return new ServiceResponse<MyTask>(task, "Task retrived succesfully", true);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<MyTask>(null, $"Error retriving task: {ex.Message}", false);
            }
        }

        public async Task<ServiceResponse<MyTask>> UpdateAsync(MyTask updateTask, string userId)
        {
            try
            {
                var task = await context.Tasks
                    .Where(x => x.UserId == userId)
                    .FirstOrDefaultAsync(x => x.Id == updateTask.Id);

                if (task is null)
                    return new ServiceResponse<MyTask>(null, "Task not found", false);

                task.Title = updateTask.Title;
                task.Description = updateTask.Description;
                task.IsDone = updateTask.IsDone;

                await context.SaveChangesAsync();

                return new ServiceResponse<MyTask>(task, "Task retrived succesfully", true);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<MyTask>(null, $"Error retriving task: {ex.Message}", false);
            }
        }
    }
}
