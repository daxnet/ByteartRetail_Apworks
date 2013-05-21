using Apworks;
using Apworks.Events;
using System;
using System.Collections.Generic;
using ByteartRetail.Domain.Model;

namespace ByteartRetail.Domain.Events
{
    [Serializable]
    public class GetUserOrdersEvent : DomainEvent
    {
        public GetUserOrdersEvent(IEntity source) : base(source) { }

        public IEnumerable<SalesOrder> SalesOrders { get; set; }
    }
}
