using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataLayer;
using DataLayer.Models;
using DataLayer.Inerfaces;
using DataLayer.Mocks;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ITaskRepository _taskRepository;

        //Do konstruktora dependency injecton wsrzyknie obiekt który implementuje interface ITaskRepository 
        //konfiguracja DI jest wewnątrz klasy startup
        public TaskController(ITaskRepository repository)
        {
            //Ten wstrzyknięty obiek przypisujemy do naszego prywatnego pola, aby mieć do niego dostęp z innych metod controllera
            _taskRepository = repository;
        }


        [HttpGet("{id}")]
        public ActionResult<ToDoTask> GetTask(int id)
        {
            //Walidujemy czy id nie jest zerem
            if (id == 0)
            {
                return BadRequest("Podaj poprawne id dla taska");
            }

            try
            {
                var task = _taskRepository.GetTask(id);
                return Ok(task);
            }
            catch (Exception ex)
            {
                return NotFound("Nie ma takiego taska z id: " + id);
            }
        }

        [HttpGet("all")]
        public List<ToDoTask> GetAllCities()
        {
            return _taskRepository.GetAllTasks();
        }

        [HttpGet("recent")]
        public ToDoTask GetRecentTask()
        {
            return _taskRepository.RecentTask();
        }

        [HttpPost]
        public ActionResult<int> AddTask(ToDoTask newTask)
        {
            //Walidacja - czy nowe zadanie ma zarówno nazwę jak i status
            if(String.IsNullOrEmpty(newTask.Name) || String.IsNullOrEmpty(newTask.Status))
            {
                //Zwracamy httpcode 400 gdy model jest niepoprawny
                return BadRequest("Nowe zadanie musi posiadać zarówno nazwę jak i status");
            }

            var task = _taskRepository.AddTask(newTask.Name, newTask.Status);
            return Created("123", task);
        }

        [HttpPut("{id}")]
        public ActionResult<int> EditTask(int id, ToDoTask newTask)
        {
            //Walidacja - czy zadanie ma zarówno nazwę jak i status
            if (String.IsNullOrEmpty(newTask.Name) || String.IsNullOrEmpty(newTask.Status))
            {
                //Zwracamy httpcode 400 gdy model jest niepoprawny
                return BadRequest("Nowe zadanie musi posiadać zarówno nazwę jak i status");
            }
            var task = _taskRepository.EditTask(id, newTask.Name, newTask.Status);
            return task;
        }

        [HttpDelete("{id}")]
        public ActionResult<int> DeleteTask(int id)
        {
            if(id == 0)
            {
                return BadRequest("Podaj poprawne id dla taska");
            }
            var task = _taskRepository.DeleteTask(id);
            return task;
        }
    }
}