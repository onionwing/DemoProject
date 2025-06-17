using Autofac;
using Onion.Demo.Application.Services;
using Onion.Demo.Infra.Data.Services;

namespace Onion.Demo.Application
{
    public class ApplicationAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<AuthenticationService>().SingleInstance();
        }
    }

}
