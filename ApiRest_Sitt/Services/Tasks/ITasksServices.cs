using Microsoft.AspNetCore.Mvc;
using ApiRest_Sitt.Models;
using System.Threading.Tasks;

namespace ApiRest_Sitt.Services.Tasks;

public interface ITasksServices
{
    Task<IEnumerable<Models.Tasks>> GetTasksAsync(int userId);
    Task<Models.Tasks> GetTaskByIdAsync(int id, int userId);
    Task<Models.Tasks> CreateTaskAsync(Models.Tasks task);
    Task<bool> UpdateTaskAsync(Models.Tasks task);
    Task<bool> DeleteTaskAsync(int id, int userId);
}
