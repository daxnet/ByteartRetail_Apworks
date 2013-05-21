using System;
using Apworks.Specifications;
using ByteartRetail.Domain.Model;

namespace ByteartRetail.Domain.Repositories.Specifications
{
    public class SalesOrderIDEqualsSpecification : Specification<SalesOrder>
    {
        private readonly Guid _orderID;

        public SalesOrderIDEqualsSpecification(Guid orderID)
        {
            _orderID = orderID;
        }

        public override System.Linq.Expressions.Expression<Func<SalesOrder, bool>> GetExpression()
        {
            return p => p.ID == _orderID;
        }
    }
}
