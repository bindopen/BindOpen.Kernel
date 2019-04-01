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
    public class ScriptWord : TAppExtensionItemConfiguration<IScriptWordDefinition>, IScriptWord
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

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
        public IScriptWord Parent { get; set; } = null;

        /// <summary>
        /// Sub script words of this instance.
        /// </summary>
        [XmlIgnore()]
        public IScriptWord SubScriptWord { get; set; } = null;

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
        /// Instantiates a new instance of the ScriptWord class.
        /// </summary>
        public ScriptWord()
            : base(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ScriptWord class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definition">The definition to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="parameterDetail">The parameter detail to consider.</param>
        protected ScriptWord(
            string name,
            IScriptWordDefinition definition = default,
            string namePreffix = "scriptWord_",
            IDataElementSet parameterDetail = null)
            : this(name, definition?.Key(), namePreffix, parameterDetail)
        {
            _definition = definition;
        }

        /// <summary>
        /// Instantiates a new instance of the ScriptWord class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="parameterDetail">The parameter detail to consider.</param>
        protected ScriptWord(
            string name,
            string definitionUniqueId,
            string namePreffix = "scriptWord_",
            IDataElementSet parameterDetail = null)
            : base(name, definitionUniqueId, namePreffix)
        {
            DefinitionUniqueId = definitionUniqueId;
            ParameterDetail = parameterDetail;
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
        public IDataElementSet NewParameterDetail()
        {
            return ParameterDetail = ParameterDetail ?? new DataElementSet();
        }

        /// <summary>
        /// Get the root script word of this instance.
        /// </summary>
        /// <returns>The root script word of this instance.</returns>
        public IScriptWord Root()
        {
            return Parent == null ? this : Parent.Root();
        }

        /// <summary>
        /// Gets the last target of this instance.
        /// </summary>
        /// <returns>Returns the last target of this instance.</returns>
        public IScriptWord Last()
        {
            return SubScriptWord == null ? this : SubScriptWord.Last();
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
            ScriptWord scriptWord = base.Clone() as ScriptWord;
            if (ParameterDetail != null)
                scriptWord.ParameterDetail= ParameterDetail.Clone() as DataElementSet;
            scriptWord.Parent = Parent;
            scriptWord.SubScriptWord = (SubScriptWord == null ? null : SubScriptWord.Clone() as ScriptWord);
            scriptWord.Item = Item;

            return scriptWord;
        }

        #endregion
    }
}
