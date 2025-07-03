using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Demo.Infra.Consul
{
    public static class ConsulConfig
    {
        public static WebApplicationBuilder AddConsulConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<ConsulServiceConfig>(builder.Configuration.GetSection("ConsulServiceConfig"));
            builder.Services.AddSingleton<IConsulClient>(sp =>
            {
                var address = builder.Configuration["Consul:Address"];
                return new ConsulClient(cfg => cfg.Address = new Uri(address!));
            });
            builder.Services.AddHostedService<ConsulRegistrationService>();

            return builder;

        }
    }
}
