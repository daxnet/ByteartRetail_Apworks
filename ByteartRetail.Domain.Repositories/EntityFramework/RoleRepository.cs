using Apworks.Repositories;
using Apworks.Repositories.EntityFramework;
using ByteartRetail.Domain.Model;

namespace ByteartRetail.Domain.Repositories.EntityFramework
{
    public class RoleRepository : EntityFrameworkRepository<Role>, IRoleRepository
    {
        public RoleRepository(IRepositoryContext context)
            : base(context)
        { }

    }
}
