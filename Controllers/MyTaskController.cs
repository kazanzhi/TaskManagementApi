using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;
using TaskManagementApi.Services;

namespace TaskManagementApi.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class MyTaskController : ControllerBase
    {
        private readonly IMyTaskService myTaskService;
        public MyTaskController(IMyTaskService myTaskService)
        {
            this.myTaskService = myTaskService;
        }

        [HttpGet("getalltask")]
        public async Task<ActionResult<List<MyTask>>> GetAll()
        {
            return Ok(await myTaskService.GetAllAsync());
        }

        [HttpGet("gettask/{id}")]
        public async Task<ActionResult<MyTask>> GetById(int id) 
        {
            return Ok(await myTaskService.GetByIdAsync(id));
        }

        [HttpPut("updatetask")]
        public async Task<ActionResult<MyTask>> Update(MyTask task)
        {
            return Ok(await myTaskService.UpdateAsync(task));
        }

        [HttpDelete("deletetask/{id}")]
        public async Task<ActionResult<List<MyTask>>> Delete(int id)
        {
            return Ok(await myTaskService.DeleteAsync(id));
        }

        [HttpPost("createtask")]
        public async Task<ActionResult<MyTask>> Create(MyTask task)
        {
            return Ok(await myTaskService.CreateAsync(task));
        }
    }
}
