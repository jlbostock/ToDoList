using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListWebApi.Domain.ViewModels
{
    public class ToDoItemVm
    {
        public int Id { get; set; }

        public string TaskName { get; set; }
    }
}
