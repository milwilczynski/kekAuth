using Autofac;
using KekAuth.Bootstrapper;
using KekBase.Initializer;

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
        services.PrepareApplication(Configuration);
        services.AddControllers();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
        });
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.UseRouting();
        app.UseCors(p => { p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute("default", "api/v1/{controller=Home}/{action=Index}/{id?}");
        });


#if DEBUG
        app.UseSwagger();
        app.UseSwaggerUI();
#endif
    }
}