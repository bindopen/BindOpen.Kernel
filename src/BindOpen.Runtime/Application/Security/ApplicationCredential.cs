using BindOpen.Data.Items;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BindOpen.Application.Security
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
        [DefaultValue("")]
        public string DomainId
        {
            get;
            set;
        }

        /// <summary>
        /// Login.
        /// </summary>
        [XmlElement("login")]
        [DefaultValue("")]
        public string Login
        {
            get;
            set;
        }

        /// <summary>
        /// Password.
        /// </summary>
        [XmlElement("password")]
        [DefaultValue("")]
        public string Password
        {
            get;
            set;
        }


        /// <summary>
        /// Token value.
        /// </summary>
        [XmlElement("tokenValue")]
        [DefaultValue("")]
        public string TokenValue
        {
            get;
            set;
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