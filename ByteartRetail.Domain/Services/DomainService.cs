using Apworks.Repositories;
using Apworks.Specifications;
using ByteartRetail.Domain.Model;
using ByteartRetail.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ByteartRetail.Domain.Services
{
    /// <summary>
    /// 表示用于Byteart Retail领域模型中的领域服务类型。
    /// </summary>
    public class DomainService : IDomainService
    {
        #region Private Fields
        private readonly IRepositoryContext _repositoryContext;
        private readonly ICategorizationRepository _categorizationRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly ISalesOrderRepository _salesOrderRepository;
        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>DomainService</c>类型的实例。
        /// </summary>
        /// <param name="repositoryContext">仓储上下文。</param>
        /// <param name="categorizationRepository">商品分类关系仓储。</param>
        /// <param name="userRoleRepository">用户角色关系仓储。</param>
        /// <param name="shoppingCartItemRepository">购物篮项目仓储。</param>
        /// <param name="salesOrderRepository">销售订单仓储。</param>
        public DomainService(IRepositoryContext repositoryContext,
            ICategorizationRepository categorizationRepository,
            IUserRoleRepository userRoleRepository,
            IShoppingCartItemRepository shoppingCartItemRepository,
            ISalesOrderRepository salesOrderRepository)
        {
            _repositoryContext = repositoryContext;
            _categorizationRepository = categorizationRepository;
            _userRoleRepository = userRoleRepository;
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _salesOrderRepository = salesOrderRepository;
        }
        #endregion

        #region IDomainService Members
        /// <summary>
        /// 将指定的商品归类到指定的商品分类中。
        /// </summary>
        /// <param name="product">需要归类的商品。</param>
        /// <param name="category">商品分类。</param>
        /// <returns>用以表述商品及其分类之间关系的实体。</returns>
        public Categorization Categorize(Product product, Category category)
        {
            if (product == null)
                throw new ArgumentNullException("product");
            if (category == null)
                throw new ArgumentNullException("category");
            var categorization = _categorizationRepository.Find(Specification<Categorization>.Eval(c => c.ProductID == product.ID));
            if (categorization == null)
            {
                categorization = Categorization.CreateCategorization(product, category);
                _categorizationRepository.Add(categorization);
            }
            else
            {
                categorization.CategoryID = category.ID;
                _categorizationRepository.Update(categorization);
            }
            _repositoryContext.Commit();
            return categorization;
        }
        /// <summary>
        /// 将指定的商品从其所属的商品分类中移除。
        /// </summary>
        /// <param name="product">商品。</param>
        /// <param name="category">分类，若为NULL，则表示从所有分类中移除。</param>
        public void Uncategorize(Product product, Category category = null)
        {
            Expression<Func<Categorization, bool>> specExpression;
            if (category == null)
                // ReSharper disable ImplicitlyCapturedClosure
                specExpression = p => p.ProductID == product.ID;
                // ReSharper restore ImplicitlyCapturedClosure
            else
                specExpression = p => p.ProductID == product.ID && p.CategoryID == category.ID;
            var categorization = _categorizationRepository.Find(Specification<Categorization>.Eval(specExpression));
            if (categorization != null)
                _categorizationRepository.Remove(categorization);
            _repositoryContext.Commit();
        }
        /// <summary>
        /// 通过指定的用户及其所拥有的购物篮实体，创建销售订单。
        /// </summary>
        /// <param name="user">用户实体。</param>
        /// <param name="shoppingCart">购物篮实体。</param>
        /// <returns>销售订单实体。</returns>
        public SalesOrder CreateSalesOrder(User user, ShoppingCart shoppingCart)
        {
            var shoppingCartItems = _shoppingCartItemRepository.FindItemsByCart(shoppingCart);
            var cartItems = shoppingCartItems as IList<ShoppingCartItem> ?? shoppingCartItems.ToList();
            if (shoppingCartItems == null ||
                !cartItems.Any())
                throw new InvalidOperationException("购物篮中没有任何物品。");

            var salesOrder = new SalesOrder {SalesLines = new List<SalesLine>()};

            foreach (var shoppingCartItem in cartItems)
            {
                var salesLine = shoppingCartItem.ConvertToSalesLine();
                salesLine.SalesOrder = salesOrder;
                salesOrder.SalesLines.Add(salesLine);
                _shoppingCartItemRepository.Remove(shoppingCartItem);
            }
            salesOrder.User = user;
            salesOrder.Status = SalesOrderStatus.Paid;
            _salesOrderRepository.Add(salesOrder);
            _repositoryContext.Commit();
            return salesOrder;
        }
        /// <summary>
        /// 将指定的用户赋予特定的角色。
        /// </summary>
        /// <param name="user">用户实体。</param>
        /// <param name="role">角色实体。</param>
        /// <returns>用以表述用户及其角色之间关系的实体。</returns>
        public UserRole AssignRole(User user, Role role)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (role == null)
                throw new ArgumentNullException("role");
            UserRole userRole = _userRoleRepository.Find(Specification<UserRole>.Eval(ur => ur.UserID == user.ID));
            if (userRole == null)
            {
                userRole = new UserRole(user.ID, role.ID);
                _userRoleRepository.Add(userRole);
            }
            else
            {
                userRole.RoleID = role.ID;
                _userRoleRepository.Update(userRole);
            }
            _repositoryContext.Commit();
            return userRole;
        }
        /// <summary>
        /// 将指定的用户从角色中移除。
        /// </summary>
        /// <param name="user">用户实体。</param>
        /// <param name="role">角色实体，若为NULL，则表示从所有角色中移除。</param>
        public void UnassignRole(User user, Role role = null)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            Expression<Func<UserRole, bool>> specExpression;
            if (role == null)
                // ReSharper disable ImplicitlyCapturedClosure
                specExpression = ur => ur.UserID == user.ID;
                // ReSharper restore ImplicitlyCapturedClosure
            else
                specExpression = ur => ur.UserID == user.ID && ur.RoleID == role.ID;

            var userRole = _userRoleRepository.Find(Specification<UserRole>.Eval(specExpression));

            if (userRole == null) return;
            _userRoleRepository.Remove(userRole);
            _repositoryContext.Commit();
        }

        #endregion
    }
}
