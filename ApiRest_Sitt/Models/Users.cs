using System.ComponentModel.DataAnnotations;

namespace ApiRest_Sitt.Models;

public class Users
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    // Relación uno a muchos con las tareas
    public ICollection<Tasks> Tasks { get; set; }
}
