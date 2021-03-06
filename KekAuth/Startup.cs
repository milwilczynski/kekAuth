using Autofac;
using KekAuth.Bootstrapper;
using KekAuth.Infrastructure.Configurations;
using KekAuth.Infrastructure.Contexts;
using KekBase.Initializer;
using Microsoft.EntityFrameworkCore;

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
        services.Configure<JwtConfiguration>(Configuration.GetSection("JwtConfiguration"));
        services.AddDbContext<KekMainContext>(options =>
        {
            options
                .UseSnakeCaseNamingConvention()
                .UseNpgsql(Configuration.GetConnectionString("Default"))
                .EnableSensitiveDataLogging();
        });
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder => builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());
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