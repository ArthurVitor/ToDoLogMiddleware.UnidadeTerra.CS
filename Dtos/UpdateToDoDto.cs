using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using ToDoList.Enums;

namespace ToDoList.Dtos;

public class UpdateToDoDto
{
    [Required]
    [NotNull]
    public string? Description { get; set; }
    [Required]
    [NotNull]
    public TodoPriority? Priority { get; set; }
}   
