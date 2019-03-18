using bdo.core.application.scopes;
using bdo.core.extensions.configuration.connectors;
using System;
using System.Xml.Serialization;

namespace bdo.standard.extension.connectors
{
    /// <summary>
    /// This class represents a remote repository connector.
    /// </summary>
    public class RepositoryConnector_NFSRemote : RepositoryConnector
   {

        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The host of this instance.
        /// </summary>
        [XmlIgnore()]
        public String Host
        {
            get
            {
                return (this.Detail.GetElementItem("Host") as String ?? "");
            }
            set
            {
                this.Detail.AddElementItem("Host", value);
            }
        }

        /// <summary>
        /// The login of this instance.
        /// </summary>
        [XmlIgnore()]
        public String Login
        {
            get
            {
                return (this.Detail.GetElementItem("Login") as String ?? "");
            }
            set
            {
                this.Detail.AddElementItem("Login", value);
            }
        }

        /// <summary>
        /// Server name of this instance.
        /// </summary>
        [XmlIgnore()]
        public String Password
        {
            get
            {
                return (this.Detail.GetElementItem("Password") as String ?? "");
            }
            set
            {
                this.Detail.AddElementItem("Password", value);
            }
        }

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RepositoryConnector_NFSRemote class.
        /// </summary>
        public RepositoryConnector_NFSRemote()
        {
        }

        /// <summary>
        /// This instantiates a new instance of the RepositoryConnector_NFSRemote class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public RepositoryConnector_NFSRemote(
            String name,
            ConnectorConfiguration configuration,
            AppScope appScope = null)
            : base(name, "file$nfsremote", configuration, appScope)
        {
        }

        #endregion

    }

}
