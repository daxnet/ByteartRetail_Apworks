using Apworks.Bus;
using Apworks.Events;
using ByteartRetail.Domain.Model;
using ByteartRetail.Domain.Repositories;

namespace ByteartRetail.Domain.Events.Handlers
{
    public class OrderDispatchedEventHandler : IDomainEventHandler<OrderDispatchedEvent>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly IEventBus _bus;

        public OrderDispatchedEventHandler(ISalesOrderRepository salesOrderRepository, IEventBus bus)
        {
            _salesOrderRepository = salesOrderRepository;
            _bus = bus;
        }

        public void Handle(OrderDispatchedEvent evnt)
        {
            var salesOrder = evnt.Source as SalesOrder;
            if (salesOrder != null)
            {
                salesOrder.DateDispatched = evnt.DispatchedDate;
                salesOrder.Status = SalesOrderStatus.Dispatched;
            }

            _bus.Publish(evnt);
        }
    }
}
