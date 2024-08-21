using ApiRest_Sitt.Data;
using ApiRest_Sitt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace ApiRest_Sitt.Services.Tasks;

public class TaskService : ITasksServices
{
    //Se crean variables para el guardado de la configuracion y el uso del Contexto de la base de datos
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    //Se crea un constructor para retornar la informacion en las variables
    public TaskService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<IEnumerable<Models.Tasks>> GetTasksAsync(int userId)
    {
        return await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
    }

    public async Task<Models.Tasks> GetTaskByIdAsync(int id, int userId)
    {
        return await _context.Tasks.FirstOrDefaultAsync(t => t.TaskId == id && t.UserId == userId);
    }

    public async Task<Models.Tasks> CreateTaskAsync(Models.Tasks task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<bool> UpdateTaskAsync(Models.Tasks task)
    {
        if (!await TaskExistsAsync(task.TaskId))
        {
            return false;
        }

        _context.Entry(task).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new Exception("Concurrencia detectada al actualizar la tarea.", ex);
        }
    }

    public async Task<bool> DeleteTaskAsync(int id, int userId)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.TaskId == id && t.UserId == userId);
        if (task == null)
        {
            return false;
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }

    private async Task<bool> TaskExistsAsync(int id)
    {
        return await _context.Tasks.AnyAsync(e => e.TaskId == id);
    }
}
