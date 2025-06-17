using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Onion.Demo.Domain.Interfaces;
using Onion.Demo.Infra.Data.Context;
using Onion.Demo.Infra.Data.UnitOfWork;

namespace Onion.Demo.WebApi.Configurations
{
    public static class EFCoreConfig
    {
        public static WebApplicationBuilder AddEFCoreConfiguration(this WebApplicationBuilder builder) {

            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.Services.AddDbContext<AppDbContext>(options => {
                string mysqlConnection = builder.Configuration.GetConnectionString("DefaultConnection")!;
                var serverVersion = ServerVersion.AutoDetect(mysqlConnection);
                options.UseMySql(mysqlConnection, serverVersion);

            });
            //builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            return builder;

        }


    }
}
