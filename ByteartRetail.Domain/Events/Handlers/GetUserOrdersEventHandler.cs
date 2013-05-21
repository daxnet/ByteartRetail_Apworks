using Apworks.Events;
using ByteartRetail.Domain.Model;
using ByteartRetail.Domain.Repositories;

namespace ByteartRetail.Domain.Events.Handlers
{
    [ParallelExecution]
    public class GetUserOrdersEventHandler : IDomainEventHandler<GetUserOrdersEvent>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;

        public GetUserOrdersEventHandler(ISalesOrderRepository salesOrderRepository)
        {
            _salesOrderRepository = salesOrderRepository;
        }

        public void Handle(GetUserOrdersEvent evnt)
        {
            var user = evnt.Source as User;
            evnt.SalesOrders = _salesOrderRepository.FindSalesOrdersByUser(user);
        }
    }
}
