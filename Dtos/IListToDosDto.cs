using System.ComponentModel.DataAnnotations;

namespace ToDoList.Dtos;

public class IListToDosDto
{
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}
