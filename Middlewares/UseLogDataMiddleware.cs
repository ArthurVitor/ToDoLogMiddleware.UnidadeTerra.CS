namespace ToDoList.Middlewares;

public static class UseLogDataMiddleware
{
    public static IApplicationBuilder UseLogData(this IApplicationBuilder app)
    {
        app.UseMiddleware<LogDataMiddleware>();
        return app;
    }
}