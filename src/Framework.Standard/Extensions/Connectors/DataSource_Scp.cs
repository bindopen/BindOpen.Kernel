using dkm.core.data.items.source;
using dkm.core.system.diagnostics;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace dkm.standard.extension.connectors.scp
{
    /// <summary>
    /// This class represents a SCP file data source.
    /// </summary>
    public class DataSource_Scp : DataSource
    {

        // ---------------------------------
        // PROPERTIES
        // ---------------------------------

        #region Properties

        /// <summary>
        /// The host of the remote server.
        /// </summary>
        [XmlElement("host")]
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
        /// Login of this instance.
        /// </summary>
        [XmlElement("login")]
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
        /// Password of this instance.
        /// </summary>
        [XmlElement("password")]
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

        /// <summary>
        /// Port of this instance.
        /// </summary>
        [XmlElement("port")]
        public int Port
        {
            get
            {
                int? aPort = this.Detail.GetElementItem("Port") as int?;
                return (aPort ?? -1);
            }
            set
            {
                this.Detail.AddElementItem("Port", value);
            }
        }

        #endregion


        // ---------------------------------
        // CONSTRUCTORS
        // ---------------------------------

        #region Constructors

        /// <summary>
        /// This instantiates a new instance of the DataSource_Scp class.
        /// </summary>
        public DataSource_Scp() 
            : this(null)
        {
        }

        /// <summary>
        /// This instantiates a new instance of the DataSource_Scp class.
        /// </summary>
        /// <param name="aDataSource">The data source to consider.</param>
        public DataSource_Scp(DataSource aDataSource)
        {
            this.ConnectorUniqueName = "standard$scp";
            this.Update(aDataSource);
        }

        #endregion


        // ---------------------------------
        // MUTATORS
        // ---------------------------------

        #region Mutators

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="aDataSource">The reference data source to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        public override Log Update(DataSource aDataSource = null)
        {
            Log aLog = base.Update(aDataSource);

            this.ConnectionString = "Provider=sqloledb;Data Source=myServerAddress;Initial Catalog=myDataBase;User Id=myUsername;Password=myPassword;";

            return aLog;
        }

        #endregion
    }

}
