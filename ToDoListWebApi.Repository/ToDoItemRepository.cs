using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDoListWebApi.Persistance.Context;
using ToDoListWebApi.Persistance.Entities;

namespace ToDoListWebApi.Repository
{
    public class ToDoItemRepository : Repository<ToDoItem, ToDoListContext>
    {
        public ToDoItemRepository(ToDoListContext toDoListContext) : base(toDoListContext) {}

        public async Task Clear()
        {
            var allToDos = await GetAll();

            foreach (var todo in allToDos)
            {
                Context.Entry(todo).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            }

            await Context.SaveChangesAsync();
        }
    }
}
