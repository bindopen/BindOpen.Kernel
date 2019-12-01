using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions.Definition.Items;
using BindOpen.Framework.Core.System.Scripting;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Runtime.Items
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

        #endregion

        // ------------------------------------------
        // CLONING
        // ------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a clone of this instance.</returns>
        public override object Clone()
        {
            IBdoScriptword scriptWord = base.Clone() as BdoScriptword;

            if (ParameterDetail != null)
                scriptWord.ParameterDetail = ParameterDetail.Clone() as DataElementSet;
            scriptWord.Parent = Parent;
            scriptWord.SubScriptword = (SubScriptword == null ? null : SubScriptword.Clone() as BdoScriptword);
            scriptWord.Item = Item;

            return scriptWord;
        }

        #endregion
    }
}
