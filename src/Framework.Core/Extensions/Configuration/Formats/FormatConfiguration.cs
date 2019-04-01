using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Definition.Formats;

namespace BindOpen.Framework.Core.Extensions.Configuration.Formats
{
    /// <summary>
    /// This class represents an format.
    /// </summary>
    [Serializable()]
    [XmlType("FormatConfiguration", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "format", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class FormatConfiguration : TAppExtensionTitledItemConfiguration<IFormatDefinition>, IFormatConfiguration
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the FormatConfiguration class.
        /// </summary>
        public FormatConfiguration()
            : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the FormatConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definition">The definition to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        protected FormatConfiguration(
            string name,
            IFormatDefinition definition = default,
            string namePreffix = "format_")
            : this(name, definition?.Key(), namePreffix)
        {
            _definition = definition;
        }

        /// <summary>
        /// Instantiates a new instance of the FormatConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        protected FormatConfiguration(
            string name,
            string definitionUniqueId,
            string namePreffix = "format_")
            : base(name, definitionUniqueId, namePreffix)
        {
            DefinitionUniqueId = definitionUniqueId;
        }

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned metrics definition.</returns>
        public override object Clone()
        {
            FormatConfiguration dataFormat = base.Clone() as FormatConfiguration;

            return dataFormat;
        }

        #endregion
    }
}
