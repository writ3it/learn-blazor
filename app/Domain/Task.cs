using System.ComponentModel.DataAnnotations;

namespace BlazorTodo.Domain;


public class Task{
    
    public int Id {get; set;}
    public string? Title { get; set; }
    public bool IsDone {get; set;}

    public void Toggle(){
        IsDone = !IsDone;
    }
}