using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Internal;
using System.Security.Claims;
using TaskManagementApi.Authentication;
using TaskManagementApi.Models;
using TaskManagementApi.Services.MyTaskServices;

namespace TaskManagementApi.Controllers
{
    [ApiController]
    [Route("/[controller]")]    
    public class MyTaskController : ControllerBase
    {
        private readonly IMyTaskService myTaskService;
        private readonly UserManager<ApplicationUser> userManager;
        public MyTaskController(IMyTaskService myTaskService, UserManager<ApplicationUser> userManager)
        {
            this.myTaskService = myTaskService;
            this.userManager = userManager;
        }

        [HttpGet("getalltask"), Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<List<MyTask>>> GetAll()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Unauthorized();

            return Ok(await myTaskService.GetAllAsync(userId));
        }

        [HttpGet("gettask/{id}"), Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<MyTask>> GetById(int id) 
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Unauthorized();

            return Ok(await myTaskService.GetByIdAsync(id, userId));
        }

        [HttpPut("updatetask"), Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<MyTask>> Update(MyTask task)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Unauthorized();

            return Ok(await myTaskService.UpdateAsync(task, userId));
        }

        [HttpDelete("deletetask/{id}"), Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<List<MyTask>>> Delete(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Unauthorized();

            return Ok(await myTaskService.DeleteAsync(id, userId));
        }

        [HttpPost("createtask"), Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<MyTask>> Create(MyTaskDto task)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Unauthorized();

            return Ok(await myTaskService.CreateAsync(task, userId));
        }
    }
}
