using BindOpen.Meta.Elements;
using BindOpen.Runtime.Definition;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Scripting
{
    /// <summary>
    /// This class represents a script word configuration.
    /// </summary>
    [XmlType("BdoScriptwordConfiguration", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "scriptword", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoScriptwordConfiguration
        : TBdoExtensionTitledItemConfiguration<IBdoScriptwordDefinition>, IBdoScriptwordConfiguration
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        /// <example>Script word, syntax, text...</example>
        public ScriptItemKinds WordKind { get; set; } = ScriptItemKinds.None;

        // Tree ----------------------------------

        /// <summary>
        /// Sub script word of this instance.
        /// </summary>
        public IBdoScriptwordConfiguration SubScriptword { get; set; }

        #endregion

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
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public new IBdoScriptwordConfiguration Add(params IBdoMetaElement[] items)
        {
            base.Add(items);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public new IBdoScriptwordConfiguration WithItems(params IBdoMetaElement[] items)
        {
            base.WithItems(items);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wordKind"></param>
        public IBdoScriptwordConfiguration WithWordKind(ScriptItemKinds wordKind)
        {
            WordKind = wordKind;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scriptword"></param>
        public IBdoScriptwordConfiguration WithSubScriptword(IBdoScriptwordConfiguration scriptword)
        {
            SubScriptword = scriptword;
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
            config.SubScriptword = SubScriptword?.Clone<BdoScriptwordConfiguration>();

            return config;
        }

        #endregion
    }
}
