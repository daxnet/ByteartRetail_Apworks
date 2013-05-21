using ByteartRetail.DataObjects;
using ByteartRetail.Infrastructure.Communication;
using ByteartRetail.ServiceContracts;
using System;
using System.Collections.Specialized;
using System.Web.Security;

namespace ByteartRetail.Web.MVC
{
    public class ByteartRetailMembershipProvider : MembershipProvider
    {
        private string _applicationName;
        private bool _enablePasswordReset;
        // ReSharper disable InconsistentNaming
        private const bool enablePasswordRetrieval = false;
        private const bool requiresQuestionAndAnswer = false;
        private const bool requiresUniqueEmail = true;
        // ReSharper restore InconsistentNaming
        private int _maxInvalidPasswordAttempts;
        private int _passwordAttemptWindow;
        private int _minRequiredPasswordLength;
        private int _minRequiredNonalphanumericCharacters;
        private string _passwordStrengthRegularExpression;
        // ReSharper disable InconsistentNaming
        private const MembershipPasswordFormat passwordFormat = MembershipPasswordFormat.Clear;
        // ReSharper restore InconsistentNaming

        private ByteartRetailMembershipUser ConvertFrom(UserDataObject userObj)
        {
            if (userObj == null)
                return null;

            var user = new ByteartRetailMembershipUser("ByteartRetailMembershipProvider",
                userObj.UserName,
                userObj.ID,
                userObj.Email,
                "",
                "",
                true,
                userObj.IsDisabled ?? true,
                userObj.DateRegistered ?? DateTime.MinValue,
                userObj.DateLastLogon ?? DateTime.MinValue,
                DateTime.MinValue,
                DateTime.MinValue,
                DateTime.MinValue,
                userObj.Contact,
                userObj.PhoneNumber,
                userObj.ContactAddress,
                userObj.DeliveryAddress);

            return user;
        }

        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (string.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }

        public override string ApplicationName
        {
            get
            {
                return _applicationName;
            }
            set
            {
                _applicationName = value;
            }
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name.Length == 0)
                name = "ByteartRetailMembershipProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Membership Provider for ByteartRetail");
            }

            base.Initialize(name, config);

            _applicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            _maxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            _passwordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
            _minRequiredNonalphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredNonalphanumericCharacters"], "1"));
            _minRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "6"));
            _enablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
            _passwordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], ""));
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotSupportedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            return false;
        }

        public ByteartRetailMembershipUser CreateUser(string username,
            string password,
            string email,
            string passwordQuestion,
            string passwordAnswer,
            bool isApproved,
            object providerUserKey,
            string contact,
            string phoneNumber,
            AddressDataObject contactAddress,
            AddressDataObject deliveryAddress,
            out MembershipCreateStatus status)
        {
            var args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);
            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }
            if (RequiresUniqueEmail && !string.IsNullOrEmpty(GetUserNameByEmail(email)))
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }
            var user = GetUser(username, true) as ByteartRetailMembershipUser;
            if (user == null)
            {
                using (var proxy = new ServiceProxy<IUserService>())
                {
                    var userDataObjects = new UserDataObjectList
                    {
                        new UserDataObject
                        {
                            UserName = username,
                            Password = password,
                            Contact = contact,
                            DateLastLogon = null,
                            DateRegistered = DateTime.Now,
                            Email = email,
                            IsDisabled = false,
                            PhoneNumber = phoneNumber,
                            ContactAddress = contactAddress,
                            DeliveryAddress = deliveryAddress
                        }
                    };
                    proxy.Channel.CreateUsers(userDataObjects);
                }
                status = MembershipCreateStatus.Success;
                return GetUser(username, true) as ByteartRetailMembershipUser;
            }
            status = MembershipCreateStatus.DuplicateUserName;
            return null;
        }

        public override MembershipUser CreateUser(string username,
            string password,
            string email,
            string passwordQuestion,
            string passwordAnswer,
            bool isApproved,
            object providerUserKey,
            out MembershipCreateStatus status)
        {
            return CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, null, null, null, null, out status);
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            using (var proxy = new ServiceProxy<IUserService>())
            {
                try
                {
                    var userDataObject = proxy.Channel.GetUserByName(username, QuerySpec.Empty);
                    proxy.Channel.DeleteUsers(new UserDataObjectList { userDataObject });
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public override bool EnablePasswordReset
        {
            get { return _enablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return enablePasswordRetrieval; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            var col = new MembershipUserCollection();
            using (var proxy = new ServiceProxy<IUserService>())
            {
                var dataObject = proxy.Channel.GetUserByEmail(emailToMatch, QuerySpec.Empty);
                totalRecords = 1;
                col.Add(ConvertFrom(dataObject));
                return col;
            }
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            var col = new MembershipUserCollection();
            using (var proxy = new ServiceProxy<IUserService>())
            {
                var dataObject = proxy.Channel.GetUserByName(usernameToMatch, QuerySpec.Empty);
                totalRecords = 1;
                col.Add(ConvertFrom(dataObject));
                return col;
            }
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            var col = new MembershipUserCollection();
            using (var proxy = new ServiceProxy<IUserService>())
            {
                var dataObjects = proxy.Channel.GetUsers(QuerySpec.Empty);
                if (dataObjects != null)
                {
                    totalRecords = dataObjects.Count;
                    foreach (var dataObject in dataObjects)
                        col.Add(ConvertFrom(dataObject));
                }
                else
                    totalRecords = 0;
                return col;
            }
        }

        public override int GetNumberOfUsersOnline()
        {
            return 0;
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            using (var proxy = new ServiceProxy<IUserService>())
            {
                var dataObject = proxy.Channel.GetUserByName(username, QuerySpec.Empty);
                if (dataObject == null)
                    return null;
                return ConvertFrom(dataObject);
            }
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            using (var proxy = new ServiceProxy<IUserService>())
            {
                var dataObject = proxy.Channel.GetUserByKey((Guid)providerUserKey, QuerySpec.Empty);
                if (dataObject == null)
                    return null;
                return ConvertFrom(dataObject);
            }
        }

        public override string GetUserNameByEmail(string email)
        {
            using (var proxy = new ServiceProxy<IUserService>())
            {
                var dataObject = proxy.Channel.GetUserByEmail(email, QuerySpec.Empty);
                if (dataObject == null)
                    return null;
                return dataObject.UserName;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return _maxInvalidPasswordAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _minRequiredNonalphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return _minRequiredPasswordLength; }
        }

        public override int PasswordAttemptWindow
        {
            get { return _passwordAttemptWindow; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return passwordFormat; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return _passwordStrengthRegularExpression; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return requiresQuestionAndAnswer; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return requiresUniqueEmail; }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotSupportedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotSupportedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            using (var proxy = new ServiceProxy<IUserService>())
            {
                return proxy.Channel.ValidateUser(username, password);
            }
        }
    }
}