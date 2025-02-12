using Microsoft.AspNetCore.Mvc;
using Entities;

[ApiController]
[Route("api/contest")]
public class TestController : ControllerBase
{
    private readonly MySqlConnectionTester _databaseService;

    public TestController(MySqlConnectionTester databaseService)
    {
        _databaseService = databaseService;
    }

    [HttpGet]
    public async Task<IActionResult> TestConnection()
    {
        var result = await _databaseService.TestMySqlConnectionAsync();
        return result ? Ok("Connection successful!") : StatusCode(500, "Connection failed");
    }
}
