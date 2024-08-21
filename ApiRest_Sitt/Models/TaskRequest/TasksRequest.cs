namespace ApiRest_Sitt.Models.TasksRequest;

public class TasksRequest
{
    public int taskId { get; set; }

    public string nombre { get; set; }

    public string descripcion { get; set; }

    public bool completado { get; set; }
}
