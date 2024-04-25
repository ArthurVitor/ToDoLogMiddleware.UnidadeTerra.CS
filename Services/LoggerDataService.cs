using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Services;

public class LoggerDataService : ILoggerData<LogToDo>
{
    private List<LogToDo> _logs = new List<LogToDo>();
    
    public LogToDo CreateToDo(String method, String path, String ip, long elapsedTime, String statusCode)
    {
        LogToDo logToDo = new(method, path, ip, elapsedTime, statusCode);
        _logs.Add(logToDo);

        return logToDo;
    }

    public IEnumerable<LogToDo> ListToDos()
    {
        return _logs;
    }
}