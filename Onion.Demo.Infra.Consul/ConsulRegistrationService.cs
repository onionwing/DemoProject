using Consul;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Onion.Demo.Infra.Consul
{
    public class ConsulRegistrationService : IHostedService
    {
        private readonly IConsulClient _client;
        private readonly ConsulServiceConfig _config;
        private string _registrationId = string.Empty;

        public ConsulRegistrationService(IConsulClient client, IOptions<ConsulServiceConfig> config)
        {
            _client = client;
            _config = config.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _registrationId = $"{_config.ServiceName}-{_config.ServiceId}";

            var registration = new AgentServiceRegistration
            {
                ID = _registrationId,
                Name = _config.ServiceName,
                Address = _config.ServiceAddress,
                Port = _config.ServicePort,
                Tags = _config.Tags,
                Check = new AgentServiceCheck
                {
                    HTTP = $"{_config.ServiceScheme}://{_config.ServiceAddress}:{_config.ServicePort}/api/health",
                    Interval = TimeSpan.FromSeconds(10),
                    Timeout = TimeSpan.FromSeconds(5),
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(30)
                }
            };

            await _client.Agent.ServiceRegister(registration, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.Agent.ServiceDeregister(_registrationId, cancellationToken);
        }
    }
}
