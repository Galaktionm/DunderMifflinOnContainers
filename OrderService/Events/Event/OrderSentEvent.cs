using RabbitMQEventBus.Abstractions;

namespace OrderService.Events.Event
{
    public class OrderSentEvent : IntegrationEvent
    {

        public OrderSentEvent(String branch)
        {

        }
    }
}
