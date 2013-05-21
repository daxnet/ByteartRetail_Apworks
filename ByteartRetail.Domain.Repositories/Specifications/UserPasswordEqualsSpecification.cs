using System;
using ByteartRetail.Domain.Model;

namespace ByteartRetail.Domain.Repositories.Specifications
{
    internal class UserPasswordEqualsSpecification : UserStringEqualsSpecification
    {

        public UserPasswordEqualsSpecification(string password)
            : base(password)
        {
        }

        public override System.Linq.Expressions.Expression<Func<User, bool>> GetExpression()
        {
            return c => c.Password == Value;
        }
    }
}
