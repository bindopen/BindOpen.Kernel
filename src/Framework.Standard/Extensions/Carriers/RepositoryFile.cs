using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Configuration.Carriers;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Standard.Extensions.Carriers
{

    /// <summary>
    /// This class represents a repository file.
    /// </summary>
    [Serializable()]
    [XmlType("File", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "file", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class RepositoryFile : RepositoryItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The length of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="length")]
        public ulong Length
        {
            get { return this.Get<ulong>(); }
            set { this.Set(value); }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RepositoryFile class.
        /// </summary>
        public RepositoryFile() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RepositoryFile class.
        /// </summary>
        /// <param name="path">The path of the instance.</param>
        public RepositoryFile(string path) : this(null, null, null)
        {
            this.Path = path;
        }

        /// <summary>
        /// Instantiates a new instance of the RepositoryFile class.
        /// </summary>
        /// <param name="fileName">The file name of the instance.</param>
        /// <param name="folderPath">The folder path of the instance.</param>
        public RepositoryFile(string fileName, string folderPath) : this(null, null, null)
        {
            this.SetPath(fileName, folderPath);
        }

        /// <summary>
        /// Instantiates a new instance of the RepositoryFile class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="relativePath">The relative path to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public RepositoryFile(
            String name,
            CarrierConfiguration configuration,
            String relativePath = null,
            AppScope appScope = null)
            : base(name, "standard$file", configuration, "file_", relativePath, appScope)
        {
        }

        #endregion


        // ------------------------------------------
        // CHECK, UPDATE, REPAIR
        // ------------------------------------------

        #region Check Repair

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="isExistenceChecked">Indicates whether the carrier existence is checked.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the check log.</returns>
        public override ILog Check<T>(
            Boolean isExistenceChecked = true,
            T item = default,
            string[] specificationAreas = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = base.Check<T>(isExistenceChecked);

            if (string.IsNullOrEmpty(this.Path))
                log.AddError("File path missing");

            return log;
        }

        #endregion

    }
}
