using AutoMapper;
using ToDoList.Dtos;
using ToDoList.Models;

namespace ToDoList.Profiles;

public class TodoProfile : Profile
{
    public TodoProfile()
    {
        CreateMap<CreateToDoDto, ToDo>(); 
        CreateMap<UpdateToDoDto, ToDo>();
    }
}
