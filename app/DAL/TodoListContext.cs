using Microsoft.EntityFrameworkCore;

namespace BlazorTodo.DAL;

using BlazorTodo.Domain;

public class TodoListContext: DbContext{
    public TodoListContext(DbContextOptions options):base(options){}

    public DbSet<Task> tasks { get; set; }
}