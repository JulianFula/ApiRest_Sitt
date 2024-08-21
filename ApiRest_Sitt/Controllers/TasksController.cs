using ApiRest_Sitt.Models;
using ApiRest_Sitt.Services.Login;
using ApiRest_Sitt.Services.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace ApiRest_Sitt.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITasksServices _tasksServices;

    // Inyección de dependencias del servicio ITaskService
    public TasksController(ITasksServices tasksServices)
    {
        _tasksServices = tasksServices;
    }

    // GET: api/Tasks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Models.Tasks>>> GetTasks()
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));  // Obtén el ID del usuario autenticado
        var tasks = await _tasksServices.GetTasksAsync(userId);  // Obtén las tareas del usuario autenticado
        return Ok(tasks);
    }

    // GET: api/Tasks/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Models.Tasks>> GetTask(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var task = await _tasksServices.GetTaskByIdAsync(id, userId);  // Obtén la tarea específica por ID

        if (task == null)
        {
            return NotFound();  // Retorna 404 si la tarea no se encuentra
        }

        return Ok(task);
    }

    // POST: api/Tasks
    [HttpPost]
    public async Task<ActionResult<Models.Tasks>> PostTask([FromBody] Models.TasksRequest.TasksRequest TasksRequest)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        Models.Tasks task = new Models.Tasks();
        task.Nombre = TasksRequest.nombre;
        task.Descripcion = TasksRequest.descripcion;
        task.Completado = TasksRequest.completado;
        task.UserId = userId;  // Asigna la tarea al usuario autenticado
        var createdTask = await _tasksServices.CreateTaskAsync(task);  // Crea la nueva tarea

        // Retorna 201 Created con la URI de la tarea creada
        return CreatedAtAction(nameof(GetTask), new { id = createdTask.TaskId }, createdTask);
    }

    // PUT: api/Tasks/5
    [HttpPut()]
    public async Task<IActionResult> PutTask([FromBody] Models.TasksRequest.TasksRequest TasksRequest)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        Models.Tasks task = new Models.Tasks();
        task.TaskId = TasksRequest.taskId;
        task.Nombre = TasksRequest.nombre;
        task.Descripcion = TasksRequest.descripcion;
        task.Completado = TasksRequest.completado;
        task.UserId = userId;  // Asigna la tarea al usuario autenticado
        var result = await _tasksServices.UpdateTaskAsync(task);  // Actualiza la tarea
        if (!result)
        {
            return NotFound();  // Retorna 404 si la tarea no existe
        }

        return NoContent();  // Retorna 204 si la actualización fue exitosa
    }

    // DELETE: api/Tasks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await _tasksServices.DeleteTaskAsync(id, userId);  // Elimina la tarea

        if (!result)
        {
            return NotFound();  // Retorna 404 si la tarea no existe
        }

        return NoContent();  // Retorna 204 si la eliminación fue exitosa
    }
}
