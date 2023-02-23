using BindOpen.Data;
using System;

namespace BindOpen.Runtime.Definitions
{
    /// <summary>
    /// This class represents the format definition.
    /// </summary>
    public class BdoFormatDefinition : BdoExtensionDefinition, IBdoFormatDefinition
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Data source kind of this instance.
        /// </summary>
        public DatasourceKind DatasourceKind { get; set; } = DatasourceKind.Memory;

        /// <summary>
        /// Item class of this instance.
        /// </summary>
        public string ItemClass { get; set; }

        /// <summary>
        /// The runtime type of this instance.
        /// </summary>
        public Type RuntimeType { get; set; }

        /// <summary>
        /// Viewer class of this instance.
        /// </summary>
        /// <remarks>Class using the following format: winForm=xxx.xxx.xxx;webForm=xxx.xxx.xxx</remarks>
        public string ViewerClass { get; set; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary>
        public new string UniqueName { get => PackageDefinition?.UniqueName + "$" + Name; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the FormatDefinition class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="extensionDefinition">The extensition definition to consider.</param>
        public BdoFormatDefinition(
            string name,
            IBdoPackageDefinition extensionDefinition) : base(name, "formatDef_", extensionDefinition)
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
            return UniqueName;
        }

        #endregion
    }
}
