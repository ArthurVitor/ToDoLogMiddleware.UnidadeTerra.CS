namespace ToDoList.Dtos;

public class ListWebToDosDto : IListToDosDto
{
    public required string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string Priority { get; set; }
    public bool IsCompleted { get; set; }
}