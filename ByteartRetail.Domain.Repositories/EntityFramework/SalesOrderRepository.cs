using Apworks.Repositories;
using Apworks.Repositories.EntityFramework;
using Apworks.Storage;
using ByteartRetail.Domain.Model;
using System;
using System.Collections.Generic;
using ByteartRetail.Domain.Repositories.Specifications;

namespace ByteartRetail.Domain.Repositories.EntityFramework
{
    public class SalesOrderRepository : EntityFrameworkRepository<SalesOrder>, ISalesOrderRepository
    {
        public SalesOrderRepository(IRepositoryContext context)
            : base(context)
        { }

        #region ISalesOrderRepository Members

        public IEnumerable<SalesOrder> FindSalesOrdersByUser(User user)
        {
            return FindAll(new SalesOrderBelongsToUserSpecification(user), sp => sp.DateCreated, SortOrder.Descending);
        }

        public SalesOrder GetSalesOrderByID(Guid orderID)
        {
            return Find(new SalesOrderIDEqualsSpecification(orderID), elp => elp.SalesLines);
        }

        #endregion
    }
}
