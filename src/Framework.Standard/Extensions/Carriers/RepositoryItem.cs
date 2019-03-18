using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Extensions.Configuration.Carriers;
using BindOpen.Framework.Core.Extensions.Runtime.Carriers;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Standard.Extensions.Carriers
{

    /// <summary>
    /// This class represents a repository item.
    /// </summary>
    [Serializable()]
    [XmlType("RepositoryItem", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "repositoryItem", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public abstract class RepositoryItem : Carrier
    {

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RepositoryItem class.
        /// </summary>
        public RepositoryItem() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RepositoryItem class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionName">The definition name to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="relativePath">The relative path to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public RepositoryItem(
            String name,
            String definitionName,
            CarrierConfiguration configuration = null,
            String namePreffix = null,
            String relativePath = null,
            AppScope appScope = null)
            : base(name, "standard$file", configuration, namePreffix, relativePath, appScope)
        {
        }

        #endregion


        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the path of this instance.
        /// </summary>
        /// <param name="path">The new path to consider. Null to update the existing one.</param>
        /// <param name="relativePath">The new relative path to consider. Null to keep the existing one.</param>
        /// <returns>Returns True if this instance exists. False otherwise.</returns>
        public override void SetPath(String path = null, String relativePath = null)
        {
            base.SetPath(path, relativePath.GetEndedString(@"\"));
        }

        #endregion

    }
}
