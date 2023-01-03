using BindOpen.Data;
using BindOpen.Data.Items;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Extensions.Scripting
{
    /// <summary>
    /// This class represents a script word.
    /// </summary>
    public class BdoScriptword :
        TBdoExtensionItem<IBdoScriptwordDefinition, IBdoScriptwordConfiguration, IBdoScriptword>,
        IBdoScriptword
    {
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

        /// <summary>
        /// Instantiates a new instance of the Scriptword class.
        /// </summary>
        /// <param name="kind"></param>
        public BdoScriptword(ScriptItemKinds kind) : base()
        {
            Kind = kind;
        }

        #endregion

        // -----------------------------------------------
        // Converters
        // -----------------------------------------------

        #region Converters

        /// <summary>
        /// Converts from string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        public static explicit operator string(BdoScriptword word)
        {
            return word?.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="st"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string operator +(string st, BdoScriptword word)
            => st + "{{" + word?.ToString() + "}}";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="st"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string operator +(BdoScriptword word, string st)
            => "{{" + word?.ToString() + "}}" + st;

        #endregion

        // -----------------------------------------------
        // IBdoScriptword Implementation
        // -----------------------------------------------

        #region IBdoScriptword

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        /// <example>Script word, syntax, text...</example>
        public ScriptItemKinds Kind { get; set; } = ScriptItemKinds.None;

        // Tree ----------------------------------

        /// <summary>
        /// Parent of this instance.
        /// </summary>
        public IBdoScriptword Parent { get; set; }

        public IBdoScriptword WithParent(IBdoScriptword scriptword)
        {
            Parent = scriptword;
            return this;
        }

        /// <summary>
        /// Sub script words of this instance.
        /// </summary>
        public IBdoScriptword SubScriptword { get; set; }

        public IBdoScriptword WithSubScriptword(IBdoScriptword scriptword)
        {
            SubScriptword = scriptword;

            return this;
        }

        // Item ----------------------------------

        /// <summary>
        /// The item of this instance that is the result of interpretation.
        /// </summary>
        public object Item { get; set; }

        public IBdoScriptword WithItem(object item)
        {
            Item = item;

            return this;
        }

        // Parameters ----------------------------------

        /// <summary>
        /// Parameters of this instance.
        /// </summary>
        public List<object> Parameters { get; set; } = new List<object>();

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

        public IBdoScriptword WithParameters(params object[] objects)
        {
            Parameters = objects.ToList();

            return this;
        }

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
        /// Returns a string that represents this instance.
        /// </summary>
        /// <returns>Retuns the string that represents this instance.</returns>
        public override string ToString()
            => ToString(true);

        /// <summary>
        /// Returns a string that represents this instance.
        /// </summary>
        /// <returns>Retuns the string that represents this instance.</returns>
        private string ToString(bool showSymbol)
        {
            string script;
            switch (Kind)
            {
                case ScriptItemKinds.Function:
                    script = string.Join(", ", Parameters?.Select(p => p.ToString(DataValueTypes.Any, true)).ToArray());
                    script = (showSymbol ? BdoScriptHelper.Symbol_Fun : "")
                        + Name + "(" + script + ")";
                    if (SubScriptword is BdoScriptword subFunScriptWord)
                    {
                        script += "." + subFunScriptWord?.ToString(false);
                    }
                    return script;
                case ScriptItemKinds.Variable:
                    script = (showSymbol ? BdoScriptHelper.Symbol_Fun : "")
                        + "('" + Name?.Replace("'", "''") + "')";
                    if (SubScriptword is BdoScriptword subVarScriptWord)
                    {
                        script += "." + subVarScriptWord?.ToString(false);
                    }
                    return script;
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
        // IDataItem Implementation
        // ------------------------------------------

        #region IDataItem Implementation

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a clone of this instance.</returns>
        public override object Clone(params string[] areas)
        {
            IBdoScriptword scriptWord = base.Clone(areas) as BdoScriptword;

            scriptWord.WithDefinition(Definition);
            scriptWord.WithConfiguration(Configuration?.Clone<BdoScriptwordConfiguration>());

            if (Parameters != null)
            {
                foreach (var paramValue in Parameters)
                {
                    scriptWord.AddParameter(paramValue);
                }
            }

            scriptWord.WithParent(Parent);
            scriptWord.WithSubScriptword(SubScriptword?.Clone<BdoScriptword>());
            scriptWord.WithItem(Item);

            return scriptWord;
        }


        #endregion

        // ------------------------------------------
        // INamedPoco Implementation
        // ------------------------------------------

        #region INamedPoco Implementation

        /// <summary>
        /// Name of this instance.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBdoScriptword WithName(string name)
        {
            Name = BdoItems.NewName(name, "word_");
            return this;
        }

        #endregion
    }
}
