using System;
using Apworks;

namespace ByteartRetail.Domain.Model
{
    /// <summary>
    /// 表示“角色”领域概念的聚合根。
    /// </summary>
    public class Role : IAggregateRoot
    {
        #region Ctor
        /// <summary>
        /// 初始化一个新的<c>Role</c>实例。
        /// </summary>
        public Role() { }
        /// <summary>
        /// 初始化一个新的<c>Role</c>实例。
        /// </summary>
        /// <param name="name">角色名称。</param>
        /// <param name="description">角色描述。</param>
        public Role(string name, string description)
        {
            Name = name;
            Description = description;
        }
        #endregion

        #region Public Properties
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置角色名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置角色描述。
        /// </summary>
        public string Description { get; set; }

        #endregion
    }
}
