using BlazorTodo.DAL;
using BlazorTodo.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlazorTodo.Infrastructure;


class TaskRepository(IDbContextFactory<TodoListContext> contextFactory) : ITaskRepository{
    private readonly IDbContextFactory<TodoListContext> contextFactory = contextFactory;

    public async System.Threading.Tasks.Task Append(Domain.Task task)
    {
        using var context = contextFactory.CreateDbContext();
        context.tasks.Add(task);
        await context.SaveChangesAsync();
    }

    public async Task<List<Domain.Task>> GetDoneTasks()
    {
        using var context = contextFactory.CreateDbContext();
        return await (from c in context.tasks
                where c.IsDone == true
                select c).ToListAsync();
    }

    public async Task<List<Domain.Task>> GetTodoTasks()
    {
        using var context = contextFactory.CreateDbContext();
        return await (from c in context.tasks
                where c.IsDone == false
                select c).ToListAsync();
    }

    public async System.Threading.Tasks.Task SaveChanges()
    {
        using var context = contextFactory.CreateDbContext();
        await context.SaveChangesAsync();
    }
}