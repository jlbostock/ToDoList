using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDoListWebApi.Domain.ViewModels;

namespace ToDoListWebApi.Domain.Contracts
{
    public interface IToDoListService
    {
        Task<ToDoItemVm> AddToDoItem(ToDoItemVm newToDoItemVm);
        Task<ToDoItemVm> RemoveToDoItem(int id);
        Task<IEnumerable<ToDoItemVm>> GetAllToDoItems();
    }
}
