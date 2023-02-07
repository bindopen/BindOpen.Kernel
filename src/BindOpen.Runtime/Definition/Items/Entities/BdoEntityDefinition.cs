using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Extensions;
using System;
using System.Collections.Generic;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents the entity definition.
    /// </summary>
    public class BdoEntityDefinition : BdoExtensionItemDefinition,
        IBdoEntityDefinition
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The data source kind of this instance.
        /// </summary>
        public DatasourceKind DatasourceKind { get; set; } = DatasourceKind.None;

        /// <summary>
        /// The set of detail specifications of this instance.
        /// </summary>
        public IBdoSpecList DetailSpec { get; set; } = new BdoSpecList();

        /// <summary>
        /// Formats of this instance.
        /// </summary>
        public List<IBdoFormatDefinition> FormatDefinitions { get; set; }

        /// <summary>
        /// Item class of this instance.
        /// </summary>
        public string ItemClass { get; set; }

        /// <summary>
        /// The kind of this instance. 
        /// </summary>
        public BdoEntityKind Kind { get; set; } = BdoEntityKind.Any;

        /// <summary>
        /// The runtime type of this instance.
        /// </summary>
        public Type RuntimeType { get; set; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary>
        public new string UniqueId { get => ExtensionDefinition?.UniqueId + "$" + Name; }

        /// <summary>
        /// Viewer class of this instance.
        /// </summary>
        public string ViewerClass { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the EntityDefinition class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="extensionDefinition">The extensition definition to consider.</param>
        public BdoEntityDefinition(
            string name,
            IBdoExtensionDefinition extensionDefinition)
            : base(name, "entityDef_", extensionDefinition)
        {
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the key of this instance.
        /// </summary>
        /// <returns></returns>
        public override string Key()
        {
            return UniqueId;
        }

        // Formats ----------------------------

        /// <summary>
        /// Gets the format with the specified unique name.
        /// </summary>
        /// <param name="uniqueName">Unique name of the application module.</param>
        /// <returns>The current visitor application module.</returns>
        public IBdoFormatDefinition GetFormatWithUniqueName(String uniqueName)
        {
            if (uniqueName == null) return null;

            return (IBdoFormatDefinition)FormatDefinitions.Find(p => p.BdoKeyEquals(uniqueName));
        }

        /// <summary>
        /// Gets the format with the specified name.
        /// </summary>
        /// <param name="name">Name of the application module.</param>
        /// <returns>The current visitor application module.</returns>
        public IBdoFormatDefinition GetFormatWithName(String name)
        {
            if (name == null) return null;

            return (IBdoFormatDefinition)FormatDefinitions.Find(p => p.BdoKeyEquals(name));
        }

        /// <summary>
        /// Gets the format with the specified ID.
        /// </summary>
        /// <param ID="id">Id of the application module.</param>
        /// <returns>The current visitor application module.</returns>
        public IBdoFormatDefinition GetFormatWithId(string uniqueId)
        {
            if (uniqueId == null) return null;

            return (IBdoFormatDefinition)FormatDefinitions.Find(p => p.UniqueId.BdoKeyEquals(uniqueId));
        }

        #endregion
    }
}
