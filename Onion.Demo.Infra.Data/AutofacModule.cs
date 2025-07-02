using Autofac;
using Onion.Demo.Infra.Data.Repositories;
using Onion.Demo.Domain.Interfaces;
using Onion.Demo.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Onion.Demo.Infra.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Onion.Demo.Infra.Data.Handler;

namespace Onion.Demo.Infra.Data
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JwtService>().SingleInstance();
            builder.RegisterType<PermissionService>().As<IPermissionService>();
            builder.RegisterType<PermissionHandler>().As<IAuthorizationHandler>().SingleInstance();

            builder.RegisterAssemblyTypes(typeof(UnitOfWork.UnitOfWork).Assembly) // 从程序集注册类型
                   .AsImplementedInterfaces().InstancePerLifetimeScope(); // 作为实现的接口注册



            //builder.RegisterType<Data.UnitOfWork.UnitOfWork>().As<IUnitOfWork>();

        }
    }
}
