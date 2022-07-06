using IntegrifyAssignment.Models;

namespace IntegrifyAssignment.Interface
{
    public interface ITodos
    {
        public bool TodoBelongsToID(long userID, long todoID);
        public Todo GetTodo(long id);
        public List<Todo> GetTodos(string? status, long userid);
        public void AddTodo(Todo todo);
        public void UpdateTodo(Todo todo);
        public void DeleteTodo(long id);

    }
}
