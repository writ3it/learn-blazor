using BlazorTodo.DAL;
using BlazorTodo.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlazorTodo.Infrastructure;


class TaskRepository(IDbContextFactory<TodoListContext> contextFactory) : ITaskRepository{
    private readonly IDbContextFactory<TodoListContext> contextFactory = contextFactory;

    private TodoListContext? context = null;

    public async System.Threading.Tasks.Task Append(Domain.Task task)
    {
        ensureContext();
        context.tasks.Add(task);
        await context.SaveChangesAsync();
    }

    private void ensureContext()
    {
        if (context != null){
            return;
        }
        context = contextFactory.CreateDbContext();
    }

    public async Task<List<Domain.Task>> GetDoneTasks()
    {
        ensureContext();
        return await (from c in context.tasks
                where c.IsDone == true
                select c).ToListAsync();
    }

    public async Task<List<Domain.Task>> GetTodoTasks()
    {
        ensureContext();
        return await (from c in context.tasks
                where c.IsDone == false
                select c).ToListAsync();
    }

    public async System.Threading.Tasks.Task SaveChanges()
    {
        ensureContext();
        await context.SaveChangesAsync();
    }

    void IDisposable.Dispose()
    {
        if (context != null){
            context.Dispose();
        }
        context = null;
    }

}