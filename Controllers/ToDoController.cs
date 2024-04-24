using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Dtos;
using AutoMapper;
using ToDoList.Enums;
using ToDoList.Interfaces;

namespace ToDoList.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoController : ControllerBase
{
    private static List<ToDo> toDos = new List<ToDo>();
    private static int id = 0;
    private IMapper _mapper;
    private ILoggerData<LogToDo> _logger;

    public ToDoController(IMapper mapper, ILoggerData<LogToDo> logger)
    {
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Adds a new to do.
    /// </summary>
    /// <param name="toDoDto">Object with necessary fields for creating a todo</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">If insertion is successful</response>
    [HttpPost]
    [ProducesResponseType(typeof(ToDo), 201)]
    [ProducesResponseType(typeof(void), 400)]
    public IActionResult AddToDo([FromBody] CreateToDoDto toDoDto)
    {
        ToDo newToDo = _mapper.Map<ToDo>(toDoDto);

        newToDo.Id = id++;
        toDos.Add(newToDo);
        return CreatedAtAction(nameof(GetToDoById), new { id = newToDo.Id },
            newToDo); 
    }

    [HttpGet("logToDos")]
    public IEnumerable<LogToDo> ListToDoLogs()
    {
        return _logger.ListToDos();
    }

    /// <summary>
    /// A to do list with pagination and filtering options.
    /// </summary>
    /// <param name="skip">Number of records to skip.</param>
    /// <param name="take">Maximum number of records to return.</param>
    /// <param name="priority">Priority of the to do.</param>
    /// <param name="isCompleted">Indicates if the to do is completed.</param>
    /// <returns>To do list.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<IListToDosDto>), 200)]
    public IEnumerable<IListToDosDto> ToDoList([FromQuery] int skip = 0, [FromQuery] int take = 20,
        [FromQuery] TodoPriority? priority = null, [FromQuery] bool? isCompleted = null)
    {
        var filteredTodos = toDos.AsQueryable();
        var userAgent = Request.Headers["User-Agent"].ToString().ToLower();
        bool isMobileRequest = userAgent.Contains("mobile"); 

        if (priority.HasValue)
        {
            filteredTodos = filteredTodos.Where(toDo => toDo.Priority == priority);
        }

        if (isCompleted.HasValue)
        {
            filteredTodos = filteredTodos.Where(toDo => toDo.IsCompleted == isCompleted);
        }

        filteredTodos = filteredTodos.Skip(skip).Take(take);

       if (isMobileRequest)
        {
            return filteredTodos.Select(toDo => new ListMobileToDosDto
            {
                Description = toDo.Description,
                IsCompleted = toDo.IsCompleted,
            });
        }
        else 
        return filteredTodos.Select(toDo => new ListWebToDosDto
        {
            Description = toDo.Description,
            CreatedAt = toDo.CreatedAt,
            Priority = toDo.GetPriorityAsString(),
            IsCompleted = toDo.IsCompleted,
        });
    }

    /// <summary>
    /// Gets a to do by its ID.
    /// </summary>
    /// <param name="id">ID of the to do.</param>
    /// <returns>The to do corresponding to the specified ID.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(IListToDosDto), 200)]
    [ProducesResponseType(typeof(void), 404)]
    public IActionResult GetToDoById(int id)
    {
        var toDo = toDos.FirstOrDefault(toDo => toDo.Id == id);
        if (toDo == null) return NotFound();
        return Ok(new ListWebToDosDto
        {
            Description = toDo.Description,
            CreatedAt = toDo.CreatedAt,
            Priority = toDo.GetPriorityAsString(),
            IsCompleted = toDo.IsCompleted,
        }); 
    }

    /// <summary>
    /// Updates the completion status of a to do.
    /// </summary>
    /// <param name="id">ID of the to do.</param>
    /// <returns>IActionResult</returns>
    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(ToDo), 200)]
    [ProducesResponseType(typeof(void), 404)]
    public IActionResult UpdateIsCompleted(int id)
    {
        var toDo = toDos.FirstOrDefault(toDo => toDo.Id == id);
        if (toDo == null) return NotFound();

        toDo.IsCompleted = !toDo.IsCompleted;
        return Ok(toDo);
    }

    /// <summary>
    /// Updates a to do.
    /// </summary>
    /// <param name="id">ID of the to do.</param>
    /// <param name="toDoDto">Object with updated to do data.</param>
    /// <returns>IActionResult</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(void), 204)]
    [ProducesResponseType(typeof(void), 404)]
    public IActionResult EditToDo(int id, [FromBody] UpdateToDoDto toDoDto)
    {
        var toDo = toDos.FirstOrDefault(toDo => toDo.Id == id);
        if (toDo == null) return NotFound();
        _mapper.Map(toDoDto, toDo);
        return NoContent(); 
    }

    /// <summary>
    /// Deletes a to do.
    /// </summary>
    /// <param name="id">ID of the 'to do' to delete.</param>
    /// <returns>IActionResult</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), 204)]
    [ProducesResponseType(typeof(void), 404)]
    public IActionResult DeleteToDo(int id)
    {
        var toDo = toDos.FirstOrDefault(toDo => toDo.Id == id);
        if (toDo == null) return NotFound();
        toDos.Remove(toDo);
        return NoContent();
    }

    /// <summary>
    /// Handles unknown requests and returns a 404 status.
    /// </summary>
    /// <returns>Status 404 (NotFound).</returns>
    [Route("{*url}", Order = int.MaxValue)]
    [HttpGet]
    [HttpPut]
    [HttpPost]
    [HttpDelete]
    [HttpPatch]
    //Changing the visibility to avoid cluttering the documentation.
    [ApiExplorerSettings(IgnoreApi = true)]
    [ProducesResponseType(typeof(void), 404)]
    public IActionResult HandleUnknownRequests()
    {
        return NotFound();
    }
}
