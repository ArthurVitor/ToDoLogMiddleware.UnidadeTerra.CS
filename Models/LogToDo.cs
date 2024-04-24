namespace ToDoList.Models;

public class LogToDo
{
    public String method { get; set; }
    
    public String path { get; set; }
    
    public String ip { get; set; }
    
    public long elapsedTime { get; set; }
    
    public String statusCode { get; set; }

    public LogToDo(string method, string path, string ip, long elapsedTime, string statusCode)
    {
        this.method = method;
        this.path = path;
        this.ip = ip;
        this.elapsedTime = elapsedTime;
        this.statusCode = statusCode;
    }
}