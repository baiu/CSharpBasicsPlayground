using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public TodosController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Todo>> GetTodos()
    {
        return _dbContext.Todos.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Todo> GetTodoById(int id)
    {
        var todo = _dbContext.Todos.Find(id);
        if (todo == null)
        {
            return NotFound();
        }
        return todo;
    }

    [HttpPost]
    public ActionResult<Todo> CreateTodo([FromBody] Todo todo)
    {
        _dbContext.Todos.Add(todo);
        _dbContext.SaveChanges();
        return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTodo(int id, [FromBody] Todo updatedTodo)
    {
        var todo = _dbContext.Todos.Find(id);
        if (todo == null)
        {
            return NotFound();
        }

        todo.Title = updatedTodo.Title;
        _dbContext.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTodo(int id)
    {
        var todo = _dbContext.Todos.Find(id);
        if (todo == null)
        {
            return NotFound();
        }

        _dbContext.Todos.Remove(todo);
        _dbContext.SaveChanges();
        return NoContent();
    }
}