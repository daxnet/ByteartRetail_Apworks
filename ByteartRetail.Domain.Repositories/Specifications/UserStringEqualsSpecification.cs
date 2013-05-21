using Apworks.Specifications;
using ByteartRetail.Domain.Model;

namespace ByteartRetail.Domain.Repositories.Specifications
{
    internal abstract class UserStringEqualsSpecification : Specification<User>
    {
        protected readonly string Value;

        protected UserStringEqualsSpecification(string value)
        {
            this.Value = value;
        }
    }
}
