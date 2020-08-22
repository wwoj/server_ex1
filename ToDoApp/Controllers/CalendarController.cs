using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Inerfaces;
using DataLayer.Mocks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private ITaskRepository _taskRepository;
        public CalendarController(ITaskRepository repository)
        {
            _taskRepository = repository;
        }

        [HttpGet]
        public ActionResult<string> GetCountOfTaskForToday()
        {
            var allTasks = _taskRepository.GetAllTasks();
            return Ok("Dzisiaj ma do zrobienia: " + allTasks.Count + " zadan");
        }
    }
}