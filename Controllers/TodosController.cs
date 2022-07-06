using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntegrifyAssignment.Models;
using IntegrifyAssignment.Interface;
using Microsoft.AspNetCore.Authorization;

namespace IntegrifyAssignment.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly IUsers _IUsers;
        private readonly ITodos _ITodos;

        public TodosController(TodoContext context, UserContext usercontext, IUsers iusers, ITodos itodos)
        {
            _IUsers = iusers;
            _ITodos = itodos;
        }

        // GET: api/Todos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos(string? status, string token)
        {
            long tokenID = _IUsers.GetIdByToken(token);
            List<Todo> todos = _ITodos.GetTodos(status, tokenID);

            return todos;
        }


        // PUT: api/Todos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(long id, Todo todo, string token)
        {
            if (id != todo.id)
            {
                return BadRequest();
            }
            long tokenID = _IUsers.GetIdByToken(token);
            bool canUpdate = _ITodos.TodoBelongsToID(tokenID,id); //making sure the request is updating a todo-item that belongs to that user!

            if (canUpdate)
            {
                todo.dateupdated = DateTime.UtcNow;
                todo.userid = tokenID;
                //_context.Entry(todo).State = EntityState.Modified;
                _ITodos.UpdateTodo(todo);
            }
            else
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Todos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(Todo todo, string token)
        {
            todo.datecreated = DateTime.UtcNow;
            todo.dateupdated = DateTime.UtcNow;
            todo.userid = _IUsers.GetIdByToken(token);
            if(todo.userid != -1) //we will not create new todoitems by users that aren't even registered
            {
                _ITodos.AddTodo(todo);
            }

            return CreatedAtAction(nameof(PostTodo), new { id = todo.id }, todo);
        }

        // DELETE: api/Todos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(long id, string token)
        {
            var todo = _ITodos.GetTodo(id);
            if (todo == null)
            {
                return NotFound();
            }
            long tokenID = _IUsers.GetIdByToken(token);
            bool canDelete = _ITodos.TodoBelongsToID(tokenID,id); //making sure the request is deleting a todo-item that belongs to that user!
            //_context.ChangeTracker.Clear();

            if (canDelete)
                _ITodos.DeleteTodo(todo.id);
            else
                return BadRequest();

            return NoContent();
        }
    }
}
