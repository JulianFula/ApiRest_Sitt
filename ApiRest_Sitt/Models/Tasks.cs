using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiRest_Sitt.Models;

public class Tasks
{
    [Key]
    public int TaskId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Nombre { get; set; }

    public string Descripcion { get; set; }

    public bool Completado { get; set; }

    // Foreign Key para relacionar la tarea con el usuario
    [ForeignKey("User")]
    public int UserId { get; set; }
    public Users User { get; set; }
}
