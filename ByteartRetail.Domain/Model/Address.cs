
namespace ByteartRetail.Domain.Model
{
    /// <summary>
    /// 表示“地址”领域概念的值对象。
    /// </summary>
    public class Address
    {
        #region Private Fields
        private string _country;
        private string _state;
        private string _city;
        private string _street;
        private string _zip;
        #endregion

        #region Public Static Fields
        /// <summary>
        /// 获取一个<c>Address</c>类型的值，该值表示一个空地址。
        /// </summary>
        public static readonly Address Emtpy = new Address(null, null, null, null, null);
        #endregion

        #region Ctor
        /// <summary>
        /// 初始化一个<c>Address</c>类型的实例。
        /// </summary>
        public Address() { }

        /// <summary>
        /// 初始化一个<c>Address</c>类型的实例。
        /// </summary>
        /// <param name="country">“地址”中的“国家”部分信息。</param>
        /// <param name="state">“地址”中的“省份/州”部分信息。</param>
        /// <param name="city">“地址”中的“市”部分信息。</param>
        /// <param name="street">“地址”中的“街道”部分信息。</param>
        /// <param name="zip">“地址”中的“邮政区码”部分信息。</param>
        public Address(string country, string state, string city, string street, string zip)
        {
            _country = country;
            _state = state;
            _city = city;
            _street = street;
            _zip = zip;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// 获取或设置“地址”类型中“国家”部分的信息。
        /// </summary>
        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }
        /// <summary>
        /// 获取或设置“地址”类型中“省份/州”部分的信息。
        /// </summary>
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }
        /// <summary>
        /// 获取或设置“地址”类型中“市”部分的信息。
        /// </summary>
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        /// <summary>
        /// 获取或设置“地址”类型中“街道”部分的信息。
        /// </summary>
        public string Street
        {
            get { return _street; }
            set { _street = value; }
        }
        /// <summary>
        /// 获取或设置“地址”类型中“邮政区码”部分的信息。
        /// </summary>
        public string Zip
        {
            get { return _zip; }
            set { _zip = value; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 确定指定的Object是否等于当前的Object。
        /// </summary>
        /// <param name="obj">要与当前对象进行比较的对象。</param>
        /// <returns>如果指定的Object与当前Object相等，则返回true，否则返回false。</returns>
        /// <remarks>有关此函数的更多信息，请参见：http://msdn.microsoft.com/zh-cn/library/system.object.equals。
        /// </remarks>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj == null)
                return false;
            var other = obj as Address;
            if ((object)other == null)
                return false;
            return _country.Equals(other._country) &&
                _state.Equals(other._state) &&
                _city.Equals(other._city) &&
                _street.Equals(other._street) &&
                _zip.Equals(other._zip);
        }

        /// <summary>
        /// 用作特定类型的哈希函数。
        /// </summary>
        /// <returns>当前Object的哈希代码。</returns>
        /// <remarks>有关此函数的更多信息，请参见：http://msdn.microsoft.com/zh-cn/library/system.object.gethashcode。
        /// </remarks>
        public override int GetHashCode()
        {
            // ReSharper disable NonReadonlyFieldInGetHashCode
            return _country.GetHashCode() ^
                _state.GetHashCode() ^
                _city.GetHashCode() ^
                _street.GetHashCode() ^
                _zip.GetHashCode();
            // ReSharper restore NonReadonlyFieldInGetHashCode
        }

        /// <summary>
        /// 返回表示当前Object的字符串。
        /// </summary>
        /// <returns>表示当前对象的字符串。</returns>
        public override string ToString()
        {
            return string.Format("{0} {1}, {2}, {3}, {4}", _zip, _street, _city, _state, _country);
        }
        #endregion

        #region Public Static Operator Overrides
        /// <summary>
        /// 确定两个“地址”对象是否相等。
        /// </summary>
        /// <param name="a">待确定的第一个“地址”对象。</param>
        /// <param name="b">待确定的另一个“地址”对象。</param>
        /// <returns>如果两者相等，则返回true，否则返回false。</returns>
        public static bool operator ==(Address a, Address b)
        {
            if ((object)a == null)
            {
                return (object)b == null;
            }
            return a.Equals(b);
        }

        /// <summary>
        /// 确定两个“地址”对象是否不相等。
        /// </summary>
        /// <param name="a">待确定的第一个“地址”对象。</param>
        /// <param name="b">待确定的另一个“地址”对象。</param>
        /// <returns>如果两者不相等，则返回true，否则返回false。</returns>
        public static bool operator !=(Address a, Address b)
        {
            return !(a == b);
        }
        #endregion
    }
}
