using BindOpen.Data.Meta;
using BindOpen.Runtime.Definition;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Scripting
{
    /// <summary>
    /// This class represents a script word configuration.
    /// </summary>
    [XmlType("BdoScriptwordConfiguration", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "scriptword", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class BdoScriptwordConfiguration
        : TBdoExtensionItemConfiguration<IBdoScriptwordDefinition>, IBdoScriptwordConfiguration
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoScriptwordConfiguration class.
        /// </summary>
        public BdoScriptwordConfiguration() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoScriptwordConfiguration class.
        /// </summary>
        public BdoScriptwordConfiguration(string definitionUniqueId)
            : base(BdoExtensionItemKind.Scriptword, definitionUniqueId)
        {
        }

        #endregion

        // ------------------------------------------
        // IBdoScriptwordConfiguration Implementation
        // ------------------------------------------

        #region IBdoScriptwordConfiguration

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public new IBdoScriptwordConfiguration Add(params IBdoMetaData[] items)
            => base.Add(items) as IBdoScriptwordConfiguration;

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param name="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public new IBdoScriptwordConfiguration WithItems(params IBdoMetaData[] items)
            => base.WithItems(items) as IBdoScriptwordConfiguration;

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        /// <example>Script word, syntax, text...</example>
        public ScriptItemKinds WordKind { get; set; } = ScriptItemKinds.None;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wordKind"></param>
        public IBdoScriptwordConfiguration WithWordKind(ScriptItemKinds wordKind)
        {
            WordKind = wordKind;
            return this;
        }

        #endregion

        // ------------------------------------------
        // CLONING
        // ------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a clone of this instance.</returns>
        public override object Clone(params string[] areas)
        {
            IBdoScriptwordConfiguration config = base.Clone(areas) as BdoScriptwordConfiguration;

            return config;
        }

        #endregion
    }
}
