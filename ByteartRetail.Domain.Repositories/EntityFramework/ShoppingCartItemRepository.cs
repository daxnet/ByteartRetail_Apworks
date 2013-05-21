using Apworks.Repositories;
using Apworks.Repositories.EntityFramework;
using Apworks.Specifications;
using ByteartRetail.Domain.Model;
using System.Collections.Generic;
using ByteartRetail.Domain.Repositories.Specifications;

namespace ByteartRetail.Domain.Repositories.EntityFramework
{
    public class ShoppingCartItemRepository : EntityFrameworkRepository<ShoppingCartItem>, IShoppingCartItemRepository
    {
        public ShoppingCartItemRepository(IRepositoryContext context)
            : base(context)
        { }

        #region IShoppingCartItemRepository Members

        public ShoppingCartItem FindItem(ShoppingCart shoppingCart, Product product)
        {
            return Find(Specification<ShoppingCartItem>.Eval(sci => sci.ShoppingCart.ID == shoppingCart.ID &&
                sci.Product.ID == product.ID), elp => elp.Product);
        }

        public IEnumerable<ShoppingCartItem> FindItemsByCart(ShoppingCart cart)
        {
            // ReSharper disable PossiblyMistakenUseOfParamsMethod
            return FindAll(new ShoppingCartItemBelongsToShoppingCartSpecification(cart), elp => elp.Product);
            // ReSharper restore PossiblyMistakenUseOfParamsMethod
        }

        #endregion
    }
}
