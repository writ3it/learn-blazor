using System.ComponentModel;
using System.Runtime.CompilerServices;
using BlazorTodo.Domain;
using Microsoft.AspNetCore.Components.Web;
using Mysqlx.Expr;

namespace BlazorTodo.Components.TaskList;


public class TodoListViewModel(ITaskRepository repository) : INotifyPropertyChanged, IDisposable
{
    private readonly ITaskRepository repository = repository;

    public event PropertyChangedEventHandler? PropertyChanged;

    public bool IsEmpty {get; set;} = true;


    public async System.Threading.Tasks.Task Initialize(){
        TodoList = await repository.GetTodoTasks();
        IsEmpty = TodoList.Count == 0;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TodoList)));
        DoneList = await repository.GetDoneTasks();
        IsEmpty = IsEmpty && DoneList.Count == 0;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DoneList)));
    }


    public List<Domain.Task> TodoList{ get; private set;} = [];

    public List<Domain.Task> DoneList{ get; private set; } = [];

    public async Task<Domain.Task> AddNewTask(string taskContent){
        var task = new Domain.Task{Title = taskContent, IsDone = false};
        await repository.Append(task);
        TodoList.Add(task);
        IsEmpty = false;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TodoList)));
        return task;
    }

    public async System.Threading.Tasks.Task TaskUpdated(Domain.Task task){
        if (task.IsDone){
            TodoList.Remove(task);
            DoneList.Add(task);
        } else {
            TodoList.Add(task);
            DoneList.Remove(task);
        }
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TodoList)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DoneList)));
        await repository.SaveChanges();
    }

    public void Dispose()
    {
        repository.Dispose();   
    }
}