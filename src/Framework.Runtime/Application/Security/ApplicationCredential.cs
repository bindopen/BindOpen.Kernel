using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Runtime.Application.Security
{
    /// <summary>
    /// This structure corresponds to a credential.
    /// </summary>
    public class ApplicationCredential : NamedDataItem
    {

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// ID of domain.
        /// </summary>
        [XmlElement("domainId")]
        public String DomainId
        {
            get;
            set;
        }

        /// <summary>
        /// Specification of the DomainId property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean DomainIdSpecified
        {
            get
            {
                return !String.IsNullOrEmpty(this.DomainId);
            }
        }

        /// <summary>
        /// Login.
        /// </summary>
        [XmlElement("login")]
        public String Login
        {
            get;
            set;
        }

        /// <summary>
        /// Specification of the Login property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean LoginSpecified
        {
            get
            {
                return !String.IsNullOrEmpty(this.Login);
            }
        }

        /// <summary>
        /// Password.
        /// </summary>
        [XmlElement("password")]
        public String Password
        {
            get;
            set;
        }

        /// <summary>
        /// Specification of the Password property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean PasswordSpecified
        {
            get
            {
                return !String.IsNullOrEmpty(this.Password);
            }
        }

        /// <summary>
        /// Token value.
        /// </summary>
        [XmlElement("tokenValue")]
        public String TokenValue
        {
            get;
            set;
        }

        /// <summary>
        /// Specification of the TokenValue property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean TokenValueSpecified
        {
            get
            {
                return !String.IsNullOrEmpty(this.TokenValue);
            }
        }

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
            if (name!=null)
                this.Name = name;
            this.DomainId = aDomainId;
            this.Login = login;
            this.Password = aPassword;
            this.TokenValue = aTokenValue;
        }

        #endregion
    }
}