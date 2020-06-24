using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Extensions.Definition;
using BindOpen.System.Scripting;
using System.Collections.Generic;
using System.Linq;
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
        /// Parameters of this instance.
        /// </summary>
        public List<object> Parameters { get; set; } = new List<object>();

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
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IBdoScriptword AddParameter(object value)
        {
            if (Parameters == null) Parameters = new List<object>();
            Parameters.Add(value);

            return this;
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
            var element = ElementFactory.Create(name);
            element.ItemScript = ToString();

            return element;
        }

        /// <summary>
        /// Returns a string that represents this instance.
        /// </summary>
        /// <returns>Retuns the string that represents this instance.</returns>
        public override string ToString()
        {
            switch (Kind)
            {
                case ScriptItemKinds.Function:
                    var script = string.Join(", ", Parameters?.Select(p => p.ToString(DataValueTypes.Any, true)).ToArray());

                    return BdoScriptHelper.Symbol_Fun + Name + "(" + script + ")";
                case ScriptItemKinds.Variable:
                    return BdoScriptHelper.Symbol_Var + Name + ")";
                case ScriptItemKinds.None:
                    return null;
                case ScriptItemKinds.Text:
                case ScriptItemKinds.Syntax:
                case ScriptItemKinds.Literal:
                case ScriptItemKinds.Any:
                default:
                    return Name;
            }
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
            //scriptWord.SetConfiguration(Configuration);
            if (Parameters != null)
            {
                foreach (var paramValue in Parameters)
                {
                    scriptWord.AddParameter(paramValue);
                }
            }

            scriptWord.Parent = Parent;
            scriptWord.SubScriptword = SubScriptword?.Clone<BdoScriptword>();
            scriptWord.Item = Item;

            return scriptWord;
        }

        #endregion
    }
}
