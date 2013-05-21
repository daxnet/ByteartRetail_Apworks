using ByteartRetail.DataObjects;
using System;
using System.Web.Security;

namespace ByteartRetail.Web.MVC
{
    /// <summary>
    /// 表示用于Byteart Retail系统的MembershipUser类型。
    /// </summary>
    public class ByteartRetailMembershipUser : MembershipUser
    {
        #region Ctor
        public ByteartRetailMembershipUser(string providerName,
            string name,
            object providerUserKey,
            string email,
            string passwordQuestion,
            string comment,
            bool isApproved,
            bool isLockedOut,
            DateTime creationDate,
            DateTime lastLoginDate,
            DateTime lastActivityDate,
            DateTime lastPasswordChangedDate,
            DateTime lastLockoutDate,
            string contact,
            string phoneNumber,
            string contactAddressCountry,
            string contactAddressState,
            string contactAddressCity,
            string contactAddressStreet,
            string contactAddressZip,
            string deliveryAddressCountry,
            string deliveryAddressState,
            string deliveryAddressCity,
            string deliveryAddressStreet,
            string deliveryAddressZip)
            : base(providerName, name, providerUserKey, email, passwordQuestion, comment, isApproved, isLockedOut, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockoutDate)
        {
            Contact = contact;
            PhoneNumber = phoneNumber;
            ContactAddressCity = contactAddressCity;
            ContactAddressCountry = contactAddressCountry;
            ContactAddressState = contactAddressState;
            ContactAddressStreet = contactAddressStreet;
            ContactAddressZip = contactAddressZip;
            DeliveryAddressCity = deliveryAddressCity;
            DeliveryAddressCountry = deliveryAddressCountry;
            DeliveryAddressState = deliveryAddressState;
            DeliveryAddressStreet = deliveryAddressStreet;
            DeliveryAddressZip = deliveryAddressZip;
        }

        public ByteartRetailMembershipUser(string providerName,
            string name,
            object providerUserKey,
            string email,
            string passwordQuestion,
            string comment,
            bool isApproved,
            bool isLockedOut,
            DateTime creationDate,
            DateTime lastLoginDate,
            DateTime lastActivityDate,
            DateTime lastPasswordChangedDate,
            DateTime lastLockoutDate,
            string contact,
            string phoneNumber,
            AddressDataObject contactAddress,
            AddressDataObject deliveryAddress)
            : this(providerName, name, providerUserKey, email, passwordQuestion, comment, isApproved, isLockedOut, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockoutDate, contact, phoneNumber,
            contactAddress == null ? null : contactAddress.Country,
            contactAddress == null ? null : contactAddress.State,
            contactAddress == null ? null : contactAddress.City,
            contactAddress == null ? null : contactAddress.Street,
            contactAddress == null ? null : contactAddress.Zip,
            deliveryAddress == null ? null : deliveryAddress.Country,
            deliveryAddress == null ? null : deliveryAddress.State,
            deliveryAddress == null ? null : deliveryAddress.City,
            deliveryAddress == null ? null : deliveryAddress.Street,
            deliveryAddress == null ? null : deliveryAddress.Zip) { }
        #endregion

        #region Public Properties
        public string Contact { get; set; }

        public string PhoneNumber { get; set; }

        public string ContactAddressCountry { get; set; }

        public string ContactAddressState { get; set; }

        public string ContactAddressCity { get; set; }

        public string ContactAddressStreet { get; set; }

        public string ContactAddressZip { get; set; }

        public string DeliveryAddressCountry { get; set; }

        public string DeliveryAddressState { get; set; }

        public string DeliveryAddressCity { get; set; }

        public string DeliveryAddressStreet { get; set; }

        public string DeliveryAddressZip { get; set; }
        #endregion

    }
}