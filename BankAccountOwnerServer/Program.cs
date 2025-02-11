using BankAccountOwnerServer.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;

var builder = WebApplication.CreateBuilder(args);

//logger service setup to be added to the container
LogManager.Setup().LoadConfigurationFromFile(string.
    Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
// Add services to the container.
builder.Services.ConfigureMySqlContext(builder.Configuration);
builder.Services.AddControllers();
builder.Services.ConfigureCors();//cors step 2
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();



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
