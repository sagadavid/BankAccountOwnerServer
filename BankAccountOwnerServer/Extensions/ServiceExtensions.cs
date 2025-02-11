using Contracts;
using LoggingService;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace BankAccountOwnerServer.Extensions
{
    public static class ServiceExtensions
    {
        //cors step 1
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {//dont want to change the default settings
            });
        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();//singleton is used to create a single instance of the logger manager

        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration configuration)
        {//iconfiguration is used to access the appsettings.json file
            //var connectionString = configuration.["mysqlconnection:connectionString"];
            var connectionString = configuration.GetConnectionString("mysqlconnection");
            //msentityframeworkcore version should suit pomelo.entityframeworkcore.mysql version, otherwise erroring services.adddbcontext 
            services.AddDbContext<RepositoryContext>(opts =>
                   opts.UseMySql(connectionString, MySqlServerVersion.LatestSupportedServerVersion));
        }
        
    }
}
