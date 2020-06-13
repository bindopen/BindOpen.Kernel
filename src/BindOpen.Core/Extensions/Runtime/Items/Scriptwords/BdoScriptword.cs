using BindOpen.Data.Elements;
using BindOpen.Extensions.Definition;
using BindOpen.System.Scripting;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// This class represents a script word.
    /// </summary>
    public class BdoScriptword : TBdoExtensionItem<IBdoScriptwordDefinition>, IBdoScriptword
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        /// <example>Script word, syntax, text...</example>
        public ScriptItemKinds Kind { get; set; } = ScriptItemKinds.None;

        // Values ----------------------------------

        /// <summary>
        /// Parameter detail of this instance.
        /// </summary>
        public IDataElementSet ParameterDetail { get; set; } = new DataElementSet();

        // Tree ----------------------------------

        /// <summary>
        /// Parent of this instance.
        /// </summary>
        [XmlIgnore()]
        public IBdoScriptword Parent { get; set; } = null;

        /// <summary>
        /// Sub script words of this instance.
        /// </summary>
        [XmlIgnore()]
        public IBdoScriptword SubScriptword { get; set; } = null;

        // Values ----------------------------------

        /// <summary>
        /// The item of this instance that is the result of interpretation.
        /// </summary>
        [XmlIgnore()]
        public object Item { get; set; }

        /// <summary>
        /// The item of this instance as a string.
        /// </summary>
        [XmlIgnore()]
        public string StringItem
        {
            get { return (Item ?? "").ToString(); }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Scriptword class.
        /// </summary>
        public BdoScriptword() : base()
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors


        /// <summary>
        /// Get the root script word of this instance.
        /// </summary>
        /// <returns>The root script word of this instance.</returns>
        public IBdoScriptword Root()
        {
            return Parent == null ? this : Parent.Root();
        }

        /// <summary>
        /// Gets the last target of this instance.
        /// </summary>
        /// <returns>Returns the last target of this instance.</returns>
        public IBdoScriptword Last()
        {
            return SubScriptword == null ? this : SubScriptword.Last();
        }

        /// <summary>
        /// Returns a data element representing this instance.
        /// </summary>
        /// <param name="name">The name of the element to create.</param>
        /// <param name="log">The log of the operation.</param>
        /// <returns>Retuns the data element that represents this instace.</returns>
        public override IDataElement AsElement(string name = null)
        {
            return null;
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
            IBdoScriptword scriptWord = base.Clone(areas) as BdoScriptword;

            scriptWord.SetDefinition(Definition);
            //scriptWord.SetConfigurtion(Configuration);
            scriptWord.ParameterDetail = ParameterDetail?.Clone<DataElementSet>();
            scriptWord.Parent = Parent;
            scriptWord.SubScriptword = SubScriptword?.Clone<BdoScriptword>();
            scriptWord.Item = Item;

            return scriptWord;
        }

        #endregion
    }
}
