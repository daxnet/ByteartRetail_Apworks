using System;
using Apworks;

namespace ByteartRetail.Domain.Model
{
    /// <summary>
    /// 表示“用户角色关系”领域概念的聚合根。
    /// </summary>
    public class UserRole : IAggregateRoot
    {
        #region Ctor
        /// <summary>
        /// 初始化一个新的<c>UserRole</c>实例。
        /// </summary>
        public UserRole() { }

        /// <summary>
        /// 初始化一个新的<c>UserRole</c>实例。
        /// </summary>
        /// <param name="userID">用户账户的ID。</param>
        /// <param name="roleID">角色的ID。</param>
        public UserRole(Guid userID, Guid roleID)
        {
            UserID = userID;
            RoleID = roleID;
        }
        #endregion

        #region Public Properties
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置用户账户的ID值。
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// 获取或设置角色的ID值。
        /// </summary>
        public Guid RoleID { get; set; }

        #endregion
    }
}
