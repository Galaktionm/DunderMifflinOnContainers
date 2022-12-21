using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQEventBus.Abstractions
{
    public class IntegrationEvent
    {
        public Guid id { get; set; }
        public DateTime createdAt { get; set; }

        public IntegrationEvent() { }

        public IntegrationEvent(Guid id, DateTime createdAt)
        {
            this.id = id;
            this.createdAt = createdAt;
        }
        

    }
}
