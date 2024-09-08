using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Application.Model.Messaging
{
    public class RabbitMqSettings
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string QueueName { get; set; }
    }

}
