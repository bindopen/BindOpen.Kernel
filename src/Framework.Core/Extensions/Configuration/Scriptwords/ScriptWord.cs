using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions.Definition.Scriptwords;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Configuration.Scriptwords
{
    /// <summary>
    /// This class represents a script word configuration.
    /// </summary>
    [Serializable()]
    public class ScriptWord : TAppExtensionItemConfiguration<ScriptWordDefinition>
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private ScriptItemKind _Kind = ScriptItemKind.None;

        // Tree ----------------------------------

        private ScriptWord _Parent = null;
        private ScriptWord _SubScriptWord = null;

        // Values ----------------------------------

        private Object _Item;

        // Values ----------------------------------

        private DataElementSet _ParameterDetail = new DataElementSet();

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        /// <example>Script word, syntax, text...</example>
        public ScriptItemKind Kind
        {
            get { return this._Kind; }
            set { this._Kind = value; } // (value.IsFunctionOrVariable() ? value : ScriptItemKind.None); }
        }

        // Values ----------------------------------

        /// <summary>
        /// Parameter detail of this instance.
        /// </summary>
        [XmlElement("parameter.detail")]
        public DataElementSet ParameterDetail
        {
            get { return this._ParameterDetail; }
            set { this._ParameterDetail = value; }
        }

        // Tree ----------------------------------

        /// <summary>
        /// Parent of this instance.
        /// </summary>
        [XmlIgnore()]
        public ScriptWord Parent
        {
            get { return this._Parent; }
            set { this._Parent = value; }
        }

        /// <summary>
        /// Sub script words of this instance.
        /// </summary>
        [XmlIgnore()]
        public ScriptWord SubScriptWord
        {
            get { return this._SubScriptWord; }
            set { this._SubScriptWord = value; }
        }

        // Values ----------------------------------

        /// <summary>
        /// The item of this instance that is the result of interpretation.
        /// </summary>
        [XmlIgnore()]
        public Object Item
        {
            get { return this._Item; }
            set { this._Item = value; }
        }

        /// <summary>
        /// The item of this instance as a string.
        /// </summary>
        [XmlIgnore()]
        public String StringItem
        {
            get { return (this._Item ?? "").ToString(); }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScriptWord class.
        /// </summary>
        public ScriptWord() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ScriptWord class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionName">The definition name to consider.</param>
        public ScriptWord(String name, String definitionName = null)
            : base(name, definitionName, null, "scriptWord_")
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the detail of this instance and instantiates it if it is null.
        /// </summary>
        /// <returns>Returns the detail of this instance.</returns>
        public DataElementSet NewParameterDetail()
        {
            return this._ParameterDetail = this._ParameterDetail ?? new DataElementSet();
        }

        /// <summary>
        /// Get the root script word of this instance.
        /// </summary>
        /// <returns>The root script word of this instance.</returns>
        public ScriptWord Root()
        {
            return (this._Parent == null ? this : this.Parent.Root());
        }

        /// <summary>
        /// Gets the last target of this instance.
        /// </summary>
        /// <returns>Returns the last target of this instance.</returns>
        public ScriptWord Last()
        {
            return (this._SubScriptWord == null ? this : this.SubScriptWord.Last());
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
        public override Object Clone()
        {
            ScriptWord scriptWord = base.Clone() as ScriptWord;
            if (this.ParameterDetail != null)
                scriptWord.ParameterDetail= this.ParameterDetail.Clone() as DataElementSet;
            scriptWord.Parent = this._Parent;
            scriptWord.SubScriptWord = (this._SubScriptWord == null ? null : this._SubScriptWord.Clone() as ScriptWord);
            scriptWord.Item = this._Item;

            return scriptWord;
        }

        #endregion
    }
}
