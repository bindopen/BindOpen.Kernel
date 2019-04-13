using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions.Definition.Scriptwords;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Items.Scriptwords
{
    /// <summary>
    /// This class represents a script word.
    /// </summary>
    [Serializable()]
    public class Scriptword : TAppExtensionItem<IScriptwordDefinition>, IScriptword
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Name of this instance.
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        /// <example>Script word, syntax, text...</example>
        public ScriptItemKind Kind { get; set; } = ScriptItemKind.None;

        // Values ----------------------------------

        /// <summary>
        /// Parameter detail of this instance.
        /// </summary>
        [XmlElement("parameter.detail")]
        public IDataElementSet ParameterDetail { get; set; } = new DataElementSet();

        // Tree ----------------------------------

        /// <summary>
        /// Parent of this instance.
        /// </summary>
        [XmlIgnore()]
        public IScriptword Parent { get; set; } = null;

        /// <summary>
        /// Sub script words of this instance.
        /// </summary>
        [XmlIgnore()]
        public IScriptword SubScriptword { get; set; } = null;

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
        public Scriptword() : base()
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
        public IScriptword Root()
        {
            return Parent == null ? this : Parent.Root();
        }

        /// <summary>
        /// Gets the last target of this instance.
        /// </summary>
        /// <returns>Returns the last target of this instance.</returns>
        public IScriptword Last()
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
            IScriptword scriptWord = base.Clone() as Scriptword;

            if (ParameterDetail != null)
                scriptWord.ParameterDetail= ParameterDetail.Clone() as DataElementSet;
            scriptWord.Parent = Parent;
            scriptWord.SubScriptword = (SubScriptword == null ? null : SubScriptword.Clone() as Scriptword);
            scriptWord.Item = Item;

            return scriptWord;
        }

        #endregion
    }
}
