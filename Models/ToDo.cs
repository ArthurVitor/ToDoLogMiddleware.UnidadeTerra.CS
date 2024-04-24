using ToDoList.Enums;

namespace ToDoList.Models;

public class ToDo
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public TodoPriority Priority { get; set; }
    public bool IsCompleted { get; set; }

    public ToDo(string description)
    {
        CreatedAt = DateTime.Now;
        IsCompleted = false;
        Description = description;
    }

    public string GetPriorityAsString()
    {
        return Enum.GetName(typeof(TodoPriority), Priority);
    }
}
