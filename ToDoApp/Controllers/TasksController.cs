using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataLayer;
using DataLayer.Models;
namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        [HttpGet]
        public ToDoTask GetTask()
        {
            var tasksRepo = new TasksRepository();
            var task = tasksRepo.GetTask();
            return task;
        }

        [HttpGet("all")]
        public List<ToDoTask> GetAllCities()
        {
            var citiesRepo = new TasksRepository();
            var cities = citiesRepo.GetAllTasks();
            return cities;
        }

        //[HttpPost]
        //public int AddTask(string name, string description)
        //{
        //    var taskRepo= new TasksRepository();
        //    var task = taskRepo.AddTask(name,description);
        //    return task;
        //}
        [HttpPost]
        public int AddTask(ToDoTask newTask)
        {
            var taskRepo = new TasksRepository();
            var task = taskRepo.AddTask(newTask.Name,newTask.Status);
            return task;
        }

        [HttpPut("{id}")]
        public int EditTask( int id,ToDoTask newTask)
        {
            var taskRepo = new TasksRepository();
            var task = taskRepo.EditTask(id,newTask.Name, newTask.Status);
            return task;
        }
        
        [HttpDelete("delete/{id}")]
        public int DeleteTask(int id)
        {
            var taskRepo = new TasksRepository();
            var task = taskRepo.DeleteTask(id);
            return task;
        }

    }
}