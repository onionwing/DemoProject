using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Demo.Infra.Consul
{
    public class ConsulServiceConfig
    {
        public string ServiceName { get; set; } = "";
        public string ServiceId { get; set; } = Guid.NewGuid().ToString();
        public string ServiceAddress { get; set; } = "localhost";
        public int ServicePort { get; set; }
        public string ServiceScheme { get; set; } = "http";
        public string[] Tags { get; set; } = Array.Empty<string>();
    }
}
