using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TODO_API_WEEK11.DTOs;
using TODO_API_WEEK11.Models;

namespace TODO_API_WEEK11.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private static List<TodoItem> todos = new List<TodoItem>();
        private static int nextId = 1;

        // GET ALL
        [HttpGet]
        public ActionResult<IEnumerable<TodoResponseDto>> GetAll()
        {
            var result = todos.Select(t => MapToResponse(t));
            return Ok(result);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public ActionResult<TodoResponseDto> GetById(int id)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);

            if (todo == null)
                return NotFound(CreateError(404, "Todo not found"));

            return Ok(MapToResponse(todo));
        }

        // CREATE
        [HttpPost]
        public ActionResult Create(CreateTodoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(CreateValidationError());

            if (string.IsNullOrWhiteSpace(dto.Title))
                return BadRequest(CreateError(400, "Title cannot be empty"));

            if (dto.DueDate.HasValue && dto.DueDate < DateTime.Now)
                return BadRequest(CreateError(400, "Due date cannot be in the past"));

            var todo = new TodoItem
            {
                Id = nextId++,
                Title = dto.Title.Trim(),
                Description = dto.Description,
                CreatedAt = DateTime.Now,
                DueDate = dto.DueDate,
                Priority = dto.Priority,
                IsCompleted = false
            };

            todos.Add(todo);

            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, MapToResponse(todo));
        }

        // UPDATE
        [HttpPut("{id}")]
        public ActionResult Update(int id, UpdateTodoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(CreateValidationError());

            var todo = todos.FirstOrDefault(t => t.Id == id);

            if (todo == null)
                return NotFound(CreateError(404, "Todo not found"));

            todo.Title = dto.Title.Trim();
            todo.Description = dto.Description;
            todo.IsCompleted = dto.IsCompleted;
            todo.DueDate = dto.DueDate;
            todo.Priority = dto.Priority;

            return Ok(MapToResponse(todo));
        }

        // DELETE
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);

            if (todo == null)
                return NotFound(CreateError(404, "Todo not found"));

            todos.Remove(todo);

            return NoContent();
        }

        // ===== Helper Methods =====

        private TodoResponseDto MapToResponse(TodoItem t)
        {
            return new TodoResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                CreatedAt = t.CreatedAt,
                DueDate = t.DueDate,
                Priority = t.Priority
            };
        }

        private ErrorResponse CreateValidationError()
        {
            return new ErrorResponse
            {
                StatusCode = 400,
                Message = "Validation failed",
                Errors = ModelState.ToDictionary(
                    x => x.Key,
                    x => x.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                ),
                Timestamp = DateTime.Now
            };
        }

        private ErrorResponse CreateError(int statusCode, string message)
        {
            return new ErrorResponse
            {
                StatusCode = statusCode,
                Message = message,
                Timestamp = DateTime.Now
            };
        }
    }
}