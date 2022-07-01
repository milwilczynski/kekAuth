using Autofac;
using KekAuth.Application.Presistances;
using KekAuth.Infrastructure.Persistances;

namespace KekAuth.Bootstrapper;

public class RepositoryModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<UserRepository>()
            .As<IUserRepository>();
    }
}