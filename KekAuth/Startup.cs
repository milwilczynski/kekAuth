using Autofac;
using KekAuth.Bootstrapper;
using KekBase.Initializer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KekAuth;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule(new ServicesModule());
        builder.RegisterModule(new RepositoryModule());
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureKekServices(Configuration);
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
        });
        services.AddOptions();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.ConfigureKekApp(loggerFactory);

#if DEBUG
        app.UseSwagger();
        app.UseSwaggerUI();
#endif
    }
}