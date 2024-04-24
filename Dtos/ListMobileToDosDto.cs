namespace ToDoList.Dtos;

public class ListMobileToDosDto : IListToDosDto
{
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}
