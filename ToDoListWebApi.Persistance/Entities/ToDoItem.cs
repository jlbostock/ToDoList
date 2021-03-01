using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListWebApi.Persistance.Entities
{
    public class ToDoItem
    {
        public ToDoItem() { }

        public ToDoItem(int id, string taskName)
        {
            Id = id;
            TaskName = taskName;
        }

        public int Id { get; set; }
        public string TaskName { get; set; }
    }
}
