using BindOpen.Framework.Data.Items;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Runtime.Application.Security
{
    /// <summary>
    /// This class represents an application credential.
    /// </summary>
    public class ApplicationCredential : NamedDataItem, IApplicationCredential
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// ID of domain.
        /// </summary>
        [XmlElement("domainId")]
        public string DomainId
        {
            get;
            set;
        }

        /// <summary>
        /// Specification of the DomainId property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool DomainIdSpecified => !string.IsNullOrEmpty(DomainId);

        /// <summary>
        /// Login.
        /// </summary>
        [XmlElement("login")]
        public string Login
        {
            get;
            set;
        }

        /// <summary>
        /// Specification of the Login property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool LoginSpecified => !string.IsNullOrEmpty(Login);

        /// <summary>
        /// Password.
        /// </summary>
        [XmlElement("password")]
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// Specification of the Password property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool PasswordSpecified => !string.IsNullOrEmpty(Password);

        /// <summary>
        /// Token value.
        /// </summary>
        [XmlElement("tokenValue")]
        public string TokenValue
        {
            get;
            set;
        }

        /// <summary>
        /// Specification of the TokenValue property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool TokenValueSpecified => !string.IsNullOrEmpty(TokenValue);

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Creates a new instance of the ApplicationCredential class.
        /// </summary>
        public ApplicationCredential()
        {
        }

        /// <summary>
        /// Creates a new instance of the ApplicationCredential class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="aDomainId">The domain ID to consider.</param>
        /// <param name="login">The login to consider.</param>
        /// <param name="aPassword">The password to consider.</param>
        /// <param name="aTokenValue">The token value to consider.</param>
        public ApplicationCredential(
            String name
            , String login = null
            , String aPassword = null
            , String aDomainId = null
            , String aTokenValue = null) : base(name, "credential_")
        {
            if (name != null)
                Name = name;
            DomainId = aDomainId;
            Login = login;
            Password = aPassword;
            TokenValue = aTokenValue;
        }

        #endregion
    }
}