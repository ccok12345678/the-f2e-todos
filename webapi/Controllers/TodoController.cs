using Microsoft.AspNetCore.Mvc;
using static webapi.Models.TodoModel;
using webapi.Models;

namespace webapi.Controllers;

[ApiController]
[Route("api/todo")]
public class TodoController : ControllerBase
{
    private string connectStr = "";

    public TodoController(IConfiguration configuration)
    {
        connectStr = configuration.GetConnectionString("MyDatabase");
    }

    [HttpGet("getall")]
    public IEnumerable<TodoItem> GetAllTodos()
    {
        var todoModel = new TodoModel(connectStr);
        return todoModel.GetAllTodoItems();
    }

    // get completed
    // get not completed


    // post add task
    // put edit task
    // put completed task

}
