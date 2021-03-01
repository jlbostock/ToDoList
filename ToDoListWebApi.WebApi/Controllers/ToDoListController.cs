using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListWebApi.Domain.Contracts;
using ToDoListWebApi.Domain.ViewModels;

namespace ToDoListWebApi.WebApi.Controllers
{
    [ApiController]
    [Route("{controller}/{action}/{id?}")]
    public class ToDoListController : ControllerBase
    {
        private readonly ILogger<ToDoListController> _logger;
        private readonly IToDoListService _toDoListService;

        public ToDoListController(ILogger<ToDoListController> logger, IToDoListService toDoListService)
        {
            _logger = logger;
            _toDoListService = toDoListService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _toDoListService.GetAllToDoItems();
            return Ok(result);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ToDoItemVm newToDoItem)
        {
            var result = await _toDoListService.AddToDoItem(newToDoItem);

            return Ok(result);
        }

        [HttpPut]
        public Task<IActionResult> Update(int id, ToDoItemVm toDoItem)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _toDoListService.RemoveToDoItem(id);

            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Clear()
        {
            throw new NotImplementedException();
        }
    }
}
