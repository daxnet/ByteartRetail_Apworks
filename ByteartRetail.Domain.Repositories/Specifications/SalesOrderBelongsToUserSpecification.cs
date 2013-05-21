using System;
using Apworks.Specifications;
using ByteartRetail.Domain.Model;

namespace ByteartRetail.Domain.Repositories.Specifications
{
    internal class SalesOrderBelongsToUserSpecification : Specification<SalesOrder>
    {
        private readonly User _user;

        public SalesOrderBelongsToUserSpecification(User user)
        {
            _user = user;
        }

        public override System.Linq.Expressions.Expression<Func<SalesOrder, bool>> GetExpression()
        {
            return so => so.User.ID == _user.ID;
        }
    }
}
