using AutoMapper;
using ToDoListWebApi.Domain.ViewModels;
using ToDoListWebApi.Persistance.Entities;

namespace ToDoListWebApi.Domain.AutoMapperProfiles
{
    public class ToDoProfile : Profile
    {
        public ToDoProfile()
        {
            CreateMap<ToDoItem, ToDoItemVm>();
            CreateMap<ToDoItemVm, ToDoItem>();
        }
    }
}
