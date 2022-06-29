using Autofac;
using KekAuth.Application.Interfaces;
using KekAuth.Application.Services;
using KekAuth.Infrastructure.Services;

namespace KekAuth.Bootstrapper;

public class ServicesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<AuthenticationService>()
            .As<IAuthenticationService>()
            .SingleInstance();

        builder
            .RegisterType<JwtTokenGenerator>()
            .As<IJwtTokenGenerator>()
            .SingleInstance();

        builder
            .RegisterType<DateTimeProvider>()
            .As<IDateTimeProvider>()
            .SingleInstance();
    }
}