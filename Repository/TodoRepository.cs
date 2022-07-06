using IntegrifyAssignment.Interface;
using IntegrifyAssignment.Models;
using Microsoft.EntityFrameworkCore;

namespace IntegrifyAssignment.Repository
{
    public class TodoRepository : ITodos
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }
        public bool TodoBelongsToID(long userID, long todoID)
        {
            Todo todo = GetTodo(todoID);
            _context.ChangeTracker.Clear(); 
            if (todo.userid == userID)
                return true;

            return false;
        }

        public void AddTodo(Todo todo)
        {
            try
            {
                _context.Todos.Add(todo);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }

        }

        public void DeleteTodo(long id)
        {
            try
            {
                Todo? todo = _context.Todos.Find(id);

                if (todo != null)
                {
                    _context.Todos.Remove(todo);
                    _context.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }

        }
        public List<Todo> GetTodos(string? status, long userid)
        {
            try
            {
                if(status == null)
                    return _context.Todos.Where(x => x.userid == userid).ToList(); //return all if status query param is NOT defined
                else
                    return _context.Todos.Where(x => x.userid == userid).Where(x => x.status == status).ToList();
            }
            catch
            {
                throw;
            }
        }

        public Todo GetTodo(long id)
        {
            try
            {
                Todo? todo = _context.Todos.Find(id);
                if (todo != null)
                {
                    return todo;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public void UpdateTodo(Todo todo)
        {
            try
            {
                _context.Entry(todo).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
