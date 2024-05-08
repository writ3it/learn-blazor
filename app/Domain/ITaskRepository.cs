
namespace BlazorTodo.Domain;


public interface ITaskRepository: IDisposable{
    public System.Threading.Tasks.Task Append(Task task);
    public Task<List<Task>> GetDoneTasks();
    public Task<List<Task>> GetTodoTasks();
    public System.Threading.Tasks.Task SaveChanges();
}