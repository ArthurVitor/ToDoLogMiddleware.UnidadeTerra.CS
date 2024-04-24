using ToDoList.Models;

namespace ToDoList.Interfaces;

public interface ILoggerData<T>
{
     T CreateToDo(String method, String path, String ip, long elapsedTime, String statusCode);
     
     IEnumerable<T> ListToDos();
}