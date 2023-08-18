using Microsoft.AspNetCore.Mvc;
using ReactProject.Models;

namespace ReactProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ReactContext _dbContext;

    public TaskController(ReactContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {

        var list = _dbContext.Tasks.OrderByDescending(t => t.IdTask).ThenBy(t => t.CreateDate).ToList();
        return await System.Threading.Tasks.Task.FromResult(StatusCode(StatusCodes.Status200OK, list));
    }

    [HttpPost]
    [Route("Save")]
    public async Task<IActionResult> Save([FromBody] Models.Task request)
    {
        await _dbContext.Tasks.AddAsync(request);
        await _dbContext.SaveChangesAsync();
        return await System.Threading.Tasks.Task.FromResult(StatusCode(StatusCodes.Status200OK, "ok"));
    }

    [HttpDelete]
    [Route("Close/{id:int}")]
    public async Task<IActionResult> Close(int id)
    {
        Models.Task? task = await _dbContext.Tasks.FindAsync(id);

        if (task is not null)
        {
            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();
        }
        return await System.Threading.Tasks.Task.FromResult(StatusCode(StatusCodes.Status200OK, "ok"));
    }
}