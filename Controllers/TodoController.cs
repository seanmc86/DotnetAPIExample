using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TodoAPI.ApiResponses;
using TodoAPI.Models;
using TodoAPI.Repository;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository todoRepository;

        public TodoController(TodoContext context)
        {
            this.todoRepository = new TodoRepository(context);
            if (todoRepository.GetItems(0, 5).Count() == 0)
            {
                todoRepository.InsertItem(
                    new TodoItem 
                    { 
                        Name = "Item1" 
                    }
                ); 
                todoRepository.Save();
            }
        }

        [HttpGet("[action]")]
        public IActionResult GetAll(int? pagenum, int? pagesize)
        {
            int pageNumber = pagenum ?? 1;
            int pageSize = pagesize ?? 5;
            return Ok(todoRepository.GetItems(pageNumber, pageSize).ToList());
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetSearch([FromQuery]int pagenum = 1, [FromQuery]int pagesize = 5, [FromQuery]string term = "")
        {
            try {
                var results = await todoRepository.GetItemsSearch(pagenum, pagesize, term);
                return Ok(new ApiSuccessResponse<List<TodoItem>>(HttpStatusCode.OK, results));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiErrorResponse(HttpStatusCode.InternalServerError, "An error occured, please try again."));
            }
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<TodoItem> GetById(long id)
        {
            var item = todoRepository.GetByID(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(new ApiSuccessResponse<TodoItem>(HttpStatusCode.OK, item));
        }

        [HttpPost]
        public ActionResult<TodoItem> Post([FromBody]TodoItem item)
        {
            todoRepository.InsertItem(item);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(new ApiSuccessResponse<TodoItem>(HttpStatusCode.OK, item));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(long id, [FromBody]TodoItem item)
        {
            item = await todoRepository.UpdateItem(id, item);
            return Ok(new ApiSuccessResponse<TodoItem>(HttpStatusCode.OK, item));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(long id)
        {
            await todoRepository.DeleteItem(id);
            return Ok(new ApiSuccessResponse<TodoItem>(HttpStatusCode.OK, null));
        }
    }
}