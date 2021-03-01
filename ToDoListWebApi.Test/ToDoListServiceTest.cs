using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListWebApi.Domain.AutoMapperProfiles;
using ToDoListWebApi.Domain.Services;
using ToDoListWebApi.Domain.ViewModels;
using ToDoListWebApi.Persistance.Context;
using ToDoListWebApi.Persistance.Entities;
using ToDoListWebApi.Repository;

namespace ToDoListWebApi.Test
{
    [TestClass]
    public class ToDoListServiceTest
    {
        public ToDoListServiceTest()
        {
            ContextOptions = new DbContextOptionsBuilder<ToDoListContext>()
                .UseInMemoryDatabase("ToDoList")
                .Options;

            Seed();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ToDoProfile());
            });

            _mapper = mapperConfig.CreateMapper();
        }

        private DbContextOptions<ToDoListContext> ContextOptions { get; }

        private IMapper _mapper;

        [TestMethod]
        public async Task GetAllToDoItems_Expected()
        {
            using (var context = new ToDoListContext(ContextOptions))
            {
                // arrange
                var toDoListService = new ToDoListService(new ToDoItemRepository(context), _mapper);

                // act
                var result = await toDoListService.GetAllToDoItems();

                // assert
                Assert.AreEqual(2, result.Count());

                var task1 = result.First();
                var task2 = result.Last();

                Assert.AreEqual("Task 1", task1.TaskName);
                Assert.AreEqual("Task 2", task2.TaskName);
            }
        }

        [TestMethod]
        public async Task AddToDoItem_Expected()
        {
            using (var context = new ToDoListContext(ContextOptions))
            {
                // arrange
                var toDoListService = new ToDoListService(new ToDoItemRepository(context), _mapper);

                var newToDo = new ToDoItemVm
                {
                    TaskName = "Task 3"
                };

                // act
                var result = await toDoListService.AddToDoItem(newToDo);

                // assert
                Assert.IsNotNull(result);
                Assert.AreEqual("Task 3", result.TaskName);

                var toDoCount = context.Set<ToDoItem>().Count();
                Assert.AreEqual(3, toDoCount);
            }
        }

        [TestMethod]
        public async Task RemoveToDoItem_Expected()
        {
            using (var context = new ToDoListContext(ContextOptions))
            {
                // arrange
                var toDoListService = new ToDoListService(new ToDoItemRepository(context), _mapper);

                const int idToRemove = 1;

                // act
                var result = await toDoListService.RemoveToDoItem(idToRemove);

                // assert
                Assert.IsNotNull(result);
                Assert.AreEqual(idToRemove, result.Id);

                var searchResult = context.Set<ToDoItem>().FirstOrDefault(x => x.Id == idToRemove);
                Assert.IsNull(searchResult);
            }
        }

        [TestMethod]
        public async Task RemoveToDoItem_DoesNotExist()
        {
            using (var context = new ToDoListContext(ContextOptions))
            {
                // arrange
                var toDoListService = new ToDoListService(new ToDoItemRepository(context), _mapper);

                const int idToRemove = 4;

                // act
                var result = await toDoListService.RemoveToDoItem(idToRemove);

                // assert
                Assert.AreEqual(default, result);
            }
        }

        private void Seed()
        {
            using (var context = new ToDoListContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Set<ToDoItem>().Add(new ToDoItem { TaskName = "Task 1" });
                context.Set<ToDoItem>().Add(new ToDoItem { TaskName = "Task 2" });

                context.SaveChanges();
            }
        }
    }
}
