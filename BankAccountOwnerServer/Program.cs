using BankAccountOwnerServer.Extensions;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using Repository;

var builder = WebApplication.CreateBuilder(args);

//logger service setup to be added to the container
LogManager.Setup().LoadConfigurationFromFile(string.
    Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.

builder.Services.ConfigureCors();//cors step 2
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureMySqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryWrapper();//repository pattern last step
builder.Services.AddTransient<MySqlConnectionTester>();//dependency injection for connection tester
builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("CorsPolicy");//cors step 3

app.UseForwardedHeaders(new ForwardedHeadersOptions { 
ForwardedHeaders = ForwardedHeaders.All});

app.UseAuthorization();

app.MapControllers();

app.Run();
