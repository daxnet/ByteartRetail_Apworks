using Apworks.Bus;
using Apworks.Events;
using ByteartRetail.Domain.Model;

namespace ByteartRetail.Domain.Events.Handlers
{
    public class OrderConfirmedEventHandler : IDomainEventHandler<OrderConfirmedEvent>
    {
        private readonly IEventBus _bus;

        public OrderConfirmedEventHandler(IEventBus bus)
        {
            _bus = bus;
        }

        public void Handle(OrderConfirmedEvent evnt)
        {
            var salesOrder = evnt.Source as SalesOrder;
            if (salesOrder != null)
            {
                salesOrder.DateDelivered = evnt.ConfirmedDate;
                salesOrder.Status = SalesOrderStatus.Delivered;
            }

            _bus.Publish(evnt);
        }
    }
}
