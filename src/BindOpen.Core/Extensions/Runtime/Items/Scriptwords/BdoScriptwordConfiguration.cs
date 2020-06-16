using BindOpen.Data.Elements;
using BindOpen.Extensions.Definition;
using BindOpen.System.Scripting;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// This class represents a script word configuration.
    /// </summary>
    [XmlType("ScriptwordConfiguration", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot("scriptword", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoScriptwordConfiguration
        : TBdoExtensionTitledItemConfiguration<BdoScriptwordDefinition>, IBdoScriptwordConfiguration
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        /// <example>Script word, syntax, text...</example>
        [XmlElement("kind")]
        public ScriptItemKinds WordKind { get; set; } = ScriptItemKinds.None;

        // Values ----------------------------------

        /// <summary>
        /// Parameter detail of this instance.
        /// </summary>
        [XmlElement("parameters")]
        public DataElementSet ParameterDetail { get; set; } = new DataElementSet();

        // Tree ----------------------------------

        /// <summary>
        /// Sub script word of this instance.
        /// </summary>
        [XmlElement("subScriptWord")]
        public BdoScriptwordConfiguration SubScriptword { get; set; } = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoScriptwordConfiguration class.
        /// </summary>
        public BdoScriptwordConfiguration() : base()
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
        public new IBdoScriptwordConfiguration Add(params IDataElement[] items)
        {
            base.Add(items);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public new IBdoScriptwordConfiguration WithItems(params IDataElement[] items)
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
        public IBdoScriptwordConfiguration WithSubScriptword(BdoScriptwordConfiguration scriptword)
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
            config.ParameterDetail = ParameterDetail?.Clone<DataElementSet>();
            config.SubScriptword = SubScriptword?.Clone<BdoScriptwordConfiguration>();

            return config;
        }

        #endregion
    }
}
