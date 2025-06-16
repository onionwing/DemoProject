using Autofac;
using Onion.Demo.Infra.Data.Repositories;
using Onion.Demo.Domain.Interfaces;
using Onion.Demo.Infra.Data.UnitOfWork;

namespace Onion.Demo.Infra.Data
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes() // 从程序集注册类型
                   .AsImplementedInterfaces(); // 作为实现的接口注册
            builder.RegisterType<Repository<>>().As<IRepository<>>();
            //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

        }
    }
}
