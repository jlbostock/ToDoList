using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoListWebApi.Persistance.Entities;

namespace ToDoListWebApi.Persistance.Context
{
    public class ToDoListContext : DbContext, IToDoListContext
    {
        public ToDoListContext(DbContextOptions<ToDoListContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoItem>().ToTable("ToDoItem");
        }
    }
}
