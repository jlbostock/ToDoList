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

        /// <summary>
        /// Add a new item to the to-do list repository
        /// </summary>
        /// <param name="newToDoItemVm"></param>
        /// <exception cref="ArgumentNullException">If newToDoItemVm param is null</exception>
        /// <returns>The new To-do item with the populated key</returns>
        public async Task<ToDoItemVm> AddToDoItem(ToDoItemVm newToDoItemVm)
        {
            if (newToDoItemVm is null) throw new ArgumentNullException(nameof(newToDoItemVm));

            if (!(newToDoItemVm.Id == default)) return null;

            var newToDoItem = _mapper.Map<ToDoItem>(newToDoItemVm);

            if (string.IsNullOrWhiteSpace(newToDoItem.TaskName)) return null;

            var result = await _todoRepo.Add(newToDoItem);

            return _mapper.Map<ToDoItemVm>(result);
        }

        /// <summary>
        /// Find and remove a to-do item, from the repository, using the ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The To-do item that was removed. Returns null if the to-do ID could not be found</returns>
        public async Task<ToDoItemVm> RemoveToDoItem(int id)
        {
            var result = await _todoRepo.Delete(id);

            return _mapper.Map<ToDoItemVm>(result);
        }

        /// <summary>
        /// Retrieve all To-do items from the repository.
        /// </summary>
        /// <returns>Enumerable of all To-do items</returns>
        public async Task<IEnumerable<ToDoItemVm>> GetAllToDoItems()
        {
            var result = await _todoRepo.GetAll();
            return _mapper.Map<IEnumerable<ToDoItemVm>>(result);
        }

        /// <summary>
        /// Removes all To-do items from the repository.
        /// </summary>
        /// <returns></returns>
        public async Task ClearList()
        {
            await _todoRepo.Clear();
        }
    }
}
