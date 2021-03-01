using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoListWebApi.Domain.Contracts;
using ToDoListWebApi.Domain.ViewModels;
using ToDoListWebApi.Repository;
using ToDoListWebApi.Persistance.Entities;
using System.Threading.Tasks;

namespace ToDoListWebApi.Domain.Services
{
    public class ToDoListService : IToDoListService
    {
        private readonly ToDoItemRepository _todoRepo;
        private readonly IMapper _mapper;

        public ToDoListService(ToDoItemRepository todoRepo, IMapper mapper)
        {
            _todoRepo = todoRepo;
            _mapper = mapper;
        }

        public async Task<ToDoItemVm> AddToDoItem(ToDoItemVm newToDoItemVm)
        {
            if (newToDoItemVm is null) throw new ArgumentNullException(nameof(newToDoItemVm));

            if (!(newToDoItemVm.Id == default)) return null;

            var newToDoItem = _mapper.Map<ToDoItem>(((ToDoItemVm)newToDoItemVm));

            if (String.IsNullOrWhiteSpace(newToDoItem.TaskName)) return null;

            var result = await _todoRepo.Add(newToDoItem);

            return _mapper.Map<ToDoItemVm>(result);
        }

        public async Task<ToDoItemVm> RemoveToDoItem(int id)
        {
            var result = await _todoRepo.Delete(id);

            return _mapper.Map<ToDoItemVm>(result);
        }

        public async Task<IEnumerable<ToDoItemVm>> GetAllToDoItems()
        {
            var result = await _todoRepo.GetAll();
            return _mapper.Map<IEnumerable<ToDoItemVm>>(result);
        }
    }
}
