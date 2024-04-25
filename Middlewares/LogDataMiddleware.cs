using System.Diagnostics;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Middlewares;

public class LogDataMiddleware
{
    private ILoggerData<LogToDo> _loggerData;

    private readonly RequestDelegate _next;
    
    public LogDataMiddleware(ILoggerData<LogToDo> loggerData, RequestDelegate next)
    {
        _loggerData = loggerData;
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (!context.Request.Path.StartsWithSegments("/api/ToDo/logToDos"))
        {
            var startTime = Stopwatch.StartNew();
            await _next(context);
            startTime.Stop();
            var timeTaken = startTime.ElapsedMilliseconds;

            var request = context.Request;
            var response = context.Response;
        
            var remoteIpAddress = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            var statusCode = response.StatusCode.ToString();
            _loggerData.CreateToDo(request.Method, request.Path, remoteIpAddress, timeTaken, statusCode);
        }
        else
        {
            await _next(context); 
        }
    }
}