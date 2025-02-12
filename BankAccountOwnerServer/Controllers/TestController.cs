using Microsoft.AspNetCore.Mvc;
using Entities;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly DatabaseService _databaseService;

    public TestController(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    [HttpGet("test-connection")]
    public async Task<IActionResult> TestConnection()
    {
        var result = await _databaseService.TestMySqlConnectionAsync();
        return result ? Ok("Connection successful!") : StatusCode(500, "Connection failed");
    }
}
