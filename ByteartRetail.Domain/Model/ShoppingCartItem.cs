using System;
using Apworks;

namespace ByteartRetail.Domain.Model
{
    /// <summary>
    /// 表示“购物篮项目”的领域实体对象。
    /// </summary>
    public class ShoppingCartItem : IAggregateRoot
    {
        #region Private Fields
        private int _quantity;
        private Product _product;
        private ShoppingCart _shoppingCart;
        #endregion

        #region Ctor
        /// <summary>
        /// 初始化一个<c>ShoppingCartItem</c>类型的实例。
        /// </summary>
        public ShoppingCartItem() { }

        /// <summary>
        /// 初始化一个<c>ShoppingCartItem</c>类型的实例。
        /// </summary>
        /// <param name="product">属于该购物篮项目的笔记本电脑商品实体。</param>
        /// <param name="shoppingCart">拥有该购物篮项目的购物篮实体。</param>
        /// <param name="quantity">拥有该购物篮项目的购物篮数量实体。</param>
        public ShoppingCartItem(Product product, ShoppingCart shoppingCart, int quantity)
        {
            _quantity = quantity;
            _product = product;
            _shoppingCart = shoppingCart;
        }
        #endregion

        #region Public Properties
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置属于当前购物篮项目的笔记本电脑商品实体。
        /// </summary>
        public virtual Product Product
        {
            get { return _product; }
            set { _product = value; }
        }

        /// <summary>
        /// 获取或设置拥有当前购物篮项目的购物篮实体。
        /// </summary>
        public virtual ShoppingCart ShoppingCart
        {
            get { return _shoppingCart; }
            set { _shoppingCart = value; }
        }

        /// <summary>
        /// 获取或设置当前购物篮项目所包含的笔记本电脑商品的个数。
        /// </summary>
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        /// <summary>
        /// 获取或设置当前购物篮项目的金额。
        /// </summary>
        /// <remarks>在严格的业务系统中，金额往往以Money模式实现。有关Money模式，请参见：http://martinfowler.com/eaaCatalog/money.html
        /// </remarks>
        public decimal LineAmount
        {
            get
            {
                return _product.UnitPrice * _quantity;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 将当前的购物篮项目转换为销售订单行。
        /// </summary>
        /// <returns></returns>
        public SalesLine ConvertToSalesLine()
        {
            var salesLine = new SalesLine
                {
                    ID = Guid.NewGuid(), 
                    Product = Product, 
                    Quantity = Quantity
                };
            return salesLine;
        }

        /// <summary>
        /// 更新当前购物篮项目所包含的笔记本电脑商品的个数。
        /// </summary>
        /// <param name="quantity">需要更新的笔记本电脑商品的个数。</param>
        public void UpdateQuantity(int quantity)
        {
            _quantity = quantity;
        }
        #endregion
    }
}
