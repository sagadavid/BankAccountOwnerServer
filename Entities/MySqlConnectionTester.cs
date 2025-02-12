using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

public class MySqlConnectionTester
{
    private readonly IConfiguration _configuration;

    public MySqlConnectionTester(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> TestMySqlConnectionAsync()
    {
        //var connectionString = _configuration.GetConnectionString("MySqlConnectionStrings");
        var connectionString = _configuration["MySqlConnectionStrings:DefaultConnection"];//fungerende maate aa hente connection string
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                Console.WriteLine("Connection successful!");
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection failed: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            return false;
        }
    }
}
