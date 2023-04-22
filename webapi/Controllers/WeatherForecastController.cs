using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using static webapi.Models.TodoModel;
using Microsoft.Extensions.Configuration;

namespace webapi.Controllers;

[ApiController]
[Route("api/weather")]
public class WeatherForecastController : ControllerBase
{
    IConfiguration _configuration;
    private string connectStr = "";

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        connectStr = _configuration.GetConnectionString("MyDataBase");
    }

    [HttpGet("forecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("testtodo")]
    public IEnumerable<TodoItem> GetAllTodos()
    {
        var todoModel = new TodoModel(connectStr);
        return todoModel.GetAllTodoItems();
    }
}
