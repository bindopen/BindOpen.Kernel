using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Extensions.Configuration.Carriers;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Standard.Extensions.Carriers
{

    /// <summary>
    /// This class represents a repository folder.
    /// </summary>
    [Serializable()]
    [XmlType("Folder", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "folder", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class RepositoryFolder : RepositoryItem
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RepositoryFolder class.
        /// </summary>
        public RepositoryFolder() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RepositoryFolder class.
        /// </summary>
        /// <param name="path">The path of the instance.</param>
        public RepositoryFolder(string path) : this(null, null)
        {
            this.Path = path;
        }

        /// <summary>
        /// Instantiates a new instance of the RepositoryFolder class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="relativePath">The relative path to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public RepositoryFolder(
            String name,
            CarrierConfiguration configuration,
            String relativePath = null,
            AppScope appScope = null)
            : base(name, "standard$folder", configuration, "folder_", relativePath, appScope)
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
        public override Log Check<T>(
            Boolean isExistenceChecked = true,
            T item = null,
            List<String> specificationAreas = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = base.Check<T>(isExistenceChecked);

            if (String.IsNullOrEmpty(this.Path))
                log.AddError("Folder path missing");

            return log;
        }

        #endregion
    }
}

