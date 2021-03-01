using System;
using System.Collections.Generic;
using System.Text;
using ToDoListWebApi.Persistance.Context;
using ToDoListWebApi.Persistance.Entities;

namespace ToDoListWebApi.Repository
{
    public class ToDoItemRepository : Repository<ToDoItem, ToDoListContext>
    {
        public ToDoItemRepository(ToDoListContext toDoListContext) : base(toDoListContext) {}


    }
}
