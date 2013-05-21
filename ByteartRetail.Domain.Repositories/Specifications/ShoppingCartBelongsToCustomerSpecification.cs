using System;
using Apworks.Specifications;
using ByteartRetail.Domain.Model;

namespace ByteartRetail.Domain.Repositories.Specifications
{
    internal class ShoppingCartBelongsToCustomerSpecification : Specification<ShoppingCart>
    {
        private readonly User _user;


        public ShoppingCartBelongsToCustomerSpecification(User user)
        {
            _user = user;
        }

        public override System.Linq.Expressions.Expression<Func<ShoppingCart, bool>> GetExpression()
        {
            return c => c.User.ID == _user.ID;
        }
    }
}
