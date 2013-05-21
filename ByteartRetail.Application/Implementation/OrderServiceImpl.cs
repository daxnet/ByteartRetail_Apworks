using System.Collections.Generic;
using Apworks;
using Apworks.Bus;
using Apworks.Repositories;
using Apworks.Storage;
using Apworks.Transactions;
using AutoMapper;
using ByteartRetail.DataObjects;
using ByteartRetail.Domain.Model;
using ByteartRetail.Domain.Repositories;
using ByteartRetail.Domain.Services;
using ByteartRetail.ServiceContracts;
using System;
using System.Linq;

namespace ByteartRetail.Application.Implementation
{
    /// <summary>
    /// 表示与“销售订单”相关的应用层服务的一种实现。
    /// </summary>
    public class OrderServiceImpl : ApplicationService, IOrderService
    {
        #region Private Fields
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly IDomainService _domainService;
        private readonly IEventBus _bus;
        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个<c>OrderServiceImpl</c>类型的实例。
        /// </summary>
        /// <param name="context">用来初始化<c>OrderServiceImpl</c>类型的仓储上下文实例。</param>
        /// <param name="shoppingCartRepository">“购物篮”仓储实例。</param>
        /// <param name="shoppingCartItemRepository">“购物篮项目”仓储实例。</param>
        /// <param name="productRepository">“笔记本电脑”仓储实例。</param>
        /// <param name="customerRepository">“客户”仓储实例。</param>
        /// <param name="salesOrderRepository">“销售订单”仓储实例。</param>
        /// <param name="domainService">领域服务实例</param>
        /// <param name="bus">事件总线实例</param>
        public OrderServiceImpl(IRepositoryContext context,
            IShoppingCartRepository shoppingCartRepository,
            IShoppingCartItemRepository shoppingCartItemRepository,
            IProductRepository productRepository,
            IUserRepository customerRepository,
            ISalesOrderRepository salesOrderRepository,
            IDomainService domainService,
            IEventBus bus)
            : base(context)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _productRepository = productRepository;
            _userRepository = customerRepository;
            _salesOrderRepository = salesOrderRepository;
            _domainService = domainService;
            _bus = bus;
        }
        #endregion

        #region IOrderService Members
        public int GetShoppingCartItemCount(Guid userID)
        {
            var user = _userRepository.GetByKey(userID);
            var shoppingCart = _shoppingCartRepository.FindShoppingCartByUser(user);
            if (shoppingCart == null)
                throw new InvalidOperationException("没有可用的购物车实例。");
            var shoppingCartItems = _shoppingCartItemRepository.FindItemsByCart(shoppingCart);
            return shoppingCartItems.Sum(s => s.Quantity);
        }

        /// <summary>
        /// 将指定的商品添加到指定客户的购物篮里。
        /// </summary>
        /// <param name="customerID">用于指代特定客户对象的全局唯一标识。</param>
        /// <param name="productID">用于指代特定商品对象的全局唯一标识。</param>
        /// <param name="quantity">商品数量</param>
        public void AddProductToCart(Guid customerID, Guid productID, int quantity)
        {
            var user = _userRepository.GetByKey(customerID);

            var shoppingCart = _shoppingCartRepository.FindShoppingCartByUser(user);
            if (shoppingCart == null)
                throw new DomainException("Shopping cart doesn't exist for customer {0}.", customerID);
            var product = _productRepository.GetByKey(productID);
            var shoppingCartItem = _shoppingCartItemRepository.FindItem(shoppingCart, product);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem(product, shoppingCart, quantity);
                _shoppingCartItemRepository.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.UpdateQuantity(shoppingCartItem.Quantity + quantity);
                _shoppingCartItemRepository.Update(shoppingCartItem);
            }
            Context.Commit();
        }

        /// <summary>
        /// 根据指定客户对象的全局唯一标识，获取该客户的购物篮信息。
        /// </summary>
        /// <param name="customerID">用于指代特定客户对象的全局唯一标识。</param>
        /// <returns>包含了购物篮信息的数据传输对象。</returns>
        public ShoppingCartDataObject GetShoppingCart(Guid customerID)
        {
            var user = _userRepository.GetByKey(customerID);

            var shoppingCart = _shoppingCartRepository.FindShoppingCartByUser(user);
            if (shoppingCart == null)
                throw new DomainException("Customer ID='{0}' doesn't have shopping cart defined.", customerID);

            var shoppingCartItems = _shoppingCartItemRepository.FindItemsByCart(shoppingCart);
            var shoppingCartDataObject = Mapper.Map<ShoppingCart, ShoppingCartDataObject>(shoppingCart);
            shoppingCartDataObject.Items = new ShoppingCartItemDataObjectList();
            var cartItems = shoppingCartItems as IList<ShoppingCartItem> ?? shoppingCartItems.ToList();
            if (shoppingCartItems != null && cartItems.Any())
            {
                foreach (var shoppingCartItem in cartItems)
                    shoppingCartDataObject.Items.Add(Mapper.Map<ShoppingCartItem, ShoppingCartItemDataObject>(shoppingCartItem));
                shoppingCartDataObject.Subtotal = shoppingCartDataObject.Items.Sum(p => p.LineAmount);
            }
            return shoppingCartDataObject;
        }

        /// <summary>
        /// 使用指定的商品数量，更新购物篮中的项目。
        /// </summary>
        /// <param name="shoppingCartItemID">需要更新的购物篮项目的全局唯一标识。</param>
        /// <param name="quantity">商品数量。</param>
        public void UpdateShoppingCartItem(Guid shoppingCartItemID, int quantity)
        {
            var shoppingCartItem = _shoppingCartItemRepository.GetByKey(shoppingCartItemID);
            shoppingCartItem.UpdateQuantity(quantity);
            _shoppingCartItemRepository.Update(shoppingCartItem);
            Context.Commit();
        }

        /// <summary>
        /// 将具有指定的全局唯一标识的购物篮项目从购物篮中删除。
        /// </summary>
        /// <param name="shoppingCartItemID">需要删除的购物篮项目的全局唯一标识。</param>
        public void DeleteShoppingCartItem(Guid shoppingCartItemID)
        {
            var shoppingCartItem = _shoppingCartItemRepository.GetByKey(shoppingCartItemID);
            _shoppingCartItemRepository.Remove(shoppingCartItem);
            Context.Commit();
        }

        /// <summary>
        /// 购物结账并产生销售订单。
        /// </summary>
        /// <param name="customerID">需要结账并生成订单的客户的全局唯一标识。</param>
        public SalesOrderDataObject Checkout(Guid customerID)
        {
            var user = _userRepository.GetByKey(customerID);
            var shoppingCart = _shoppingCartRepository.FindShoppingCartByUser(user);

            var salesOrder = _domainService.CreateSalesOrder(user, shoppingCart);

            return Mapper.Map<SalesOrder, SalesOrderDataObject>(salesOrder);
        }

        /// <summary>
        /// 销售订单确认。
        /// </summary>
        /// <param name="orderID">需要被确认的销售订单的全局唯一标识。</param>
        public void Confirm(Guid orderID)
        {
            using (ITransactionCoordinator coordinator = TransactionCoordinatorFactory.Create(Context, _bus))
            {
                var salesOrder = _salesOrderRepository.GetByKey(orderID);
                salesOrder.Confirm();
                _salesOrderRepository.Update(salesOrder);
                coordinator.Commit();
            }
        }

        public void Dispatch(Guid orderID)
        {
            using (ITransactionCoordinator coordinator = TransactionCoordinatorFactory.Create(Context, _bus))
            {
                var salesOrder = _salesOrderRepository.GetByKey(orderID);
                salesOrder.Dispatch();
                _salesOrderRepository.Update(salesOrder);
                coordinator.Commit();
            }
        }

        public SalesOrderDataObjectList GetSalesOrdersForUser(Guid userID)
        {
            var user = _userRepository.GetByKey(userID);
            var salesOrders = _salesOrderRepository.FindSalesOrdersByUser(user);
            var salesOrderDataObjectList = new SalesOrderDataObjectList();
            salesOrderDataObjectList.AddRange(salesOrders.Select(Mapper.Map<SalesOrder, SalesOrderDataObject>));
            return salesOrderDataObjectList;
        }

        public SalesOrderDataObjectList GetAllSalesOrders()
        {
            var salesOrders = _salesOrderRepository.FindAll(sort => sort.DateCreated, SortOrder.Descending);
            var result = new SalesOrderDataObjectList();
            salesOrders
                .ToList()
                .ForEach(p => result.Add(Mapper.Map<SalesOrder, SalesOrderDataObject>(p)));
            return result;
        }

        /// <summary>
        /// 根据指定的全局唯一标识获取销售订单信息。
        /// </summary>
        /// <param name="orderID">销售订单全局唯一标识。</param>
        /// <returns>包含销售订单信息的数据传输对象。</returns>
        public SalesOrderDataObject GetSalesOrder(Guid orderID)
        {
            //var salesOrder = salesOrderRepository.Get(Specification<SalesOrder>.Eval(so => so.ID == orderID), elp => elp.SalesLines);
            var salesOrder = _salesOrderRepository.GetSalesOrderByID(orderID);
            return Mapper.Map<SalesOrder, SalesOrderDataObject>(salesOrder);
        }

        #endregion


    }
}
