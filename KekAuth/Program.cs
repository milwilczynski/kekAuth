using Autofac.Extensions.DependencyInjection;
using Serilog;

namespace KekAuth;

public class Program
{
    public static void Main(string[] args)
    {
        HostBuilder(args).Run();
    }

    public static IHost HostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureWebHostDefaults(webHostBuilder =>
            {
                webHostBuilder
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseStartup<Startup>();
            })
            .Build();
    }
}