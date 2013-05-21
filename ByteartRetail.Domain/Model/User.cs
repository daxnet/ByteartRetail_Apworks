using Apworks;
using Apworks.Events;
using System;
using System.Collections.Generic;
using ByteartRetail.Domain.Events;

namespace ByteartRetail.Domain.Model
{
    /// <summary>
    /// 表示“用户”领域概念的聚合根。
    /// </summary>
    public class User : IAggregateRoot
    {
        #region Private Fields

        public User()
        {
            DeliveryAddress = Address.Emtpy;
            ContactAddress = Address.Emtpy;
        }

        #endregion


        #region Public Properties
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置当前客户的用户名。
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置当前客户的登录密码。
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置当前客户的电子邮件地址。
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 获取或设置一个<see cref="Boolean"/>值，该值表示当前用户账户是否已被禁用。
        /// </summary>
        /// <remarks>在ByteartRetail V3中，仅提供对此属性的管理界面，在实际业务处理中
        /// 并没有使用到该属性。</remarks>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 获取或设置用户账户注册的时间。
        /// </summary>
        public DateTime DateRegistered { get; set; }

        /// <summary>
        /// 获取或设置用户账户最后一次登录的时间。
        /// </summary>
        /// <remarks>在ByteartRetail V3中，仅提供对此属性的管理界面，在实际业务处理中
        /// 并没有使用到该属性。</remarks>
        public DateTime? DateLastLogon { get; set; }

        /// <summary>
        /// 获取或设置当前客户的联系人信息。
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 获取或设置用户账户的联系电话信息。
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 获取或设置用户账户的联系地址。
        /// </summary>
        public Address ContactAddress { get; set; }

        /// <summary>
        /// 获取或设置用户账户的发货地址。
        /// </summary>
        public Address DeliveryAddress { get; set; }

        public IEnumerable<SalesOrder> SalesOrders
        {
            get
            {
                IEnumerable<SalesOrder> result = null;
                DomainEvent.Publish(new GetUserOrdersEvent(this),
                    (e, ret, exc) =>
                    {
                        result = e.SalesOrders;
                    });
                return result;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 返回表示当前Object的字符串。
        /// </summary>
        /// <returns>表示当前对象的字符串。</returns>
        public override string ToString()
        {
            return UserName;
        }

        /// <summary>
        /// 禁用当前账户。
        /// </summary>
        public void Disable()
        {
            IsDisabled = true;
        }

        /// <summary>
        /// 启用当前账户。
        /// </summary>
        public void Enable()
        {
            IsDisabled = false;
        }

        /// <summary>
        /// 为当前用户创建购物篮。
        /// </summary>
        /// <returns>已创建的购物篮实例，该购物篮为当前用户所用。</returns>
        public ShoppingCart CreateShoppingCart()
        {
            var shoppingCart = new ShoppingCart {User = this};
            return shoppingCart;
        }
        #endregion
    }
}
