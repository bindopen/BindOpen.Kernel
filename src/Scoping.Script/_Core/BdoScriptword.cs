using BindOpen.System.Data;
using BindOpen.System.Data.Conditions;
using BindOpen.System.Data.Helpers;
using BindOpen.System.Data.Meta;
using System.Linq;

namespace BindOpen.System.Scoping.Script
{
    /// <summary>
    /// This class represents a script word.
    /// </summary>
    public class BdoScriptword : BdoMetaObject, IBdoScriptword
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
            this.WithDataType(DataValueTypes.Scriptword);
        }

        #endregion

        // -----------------------------------------------
        // Converters
        // -----------------------------------------------

        #region Converters

        /// <summary>
        /// Converts from string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        public static implicit operator BdoExpression(BdoScriptword word)
        {
            return BdoData.NewExp(word);
        }

        /// <summary>
        /// Converts from string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        public static implicit operator BdoCondition(BdoScriptword word)
        {
            return BdoData.NewCondition((BdoExpression)word);
        }

        /// <summary>
        /// Converts from word.
        /// </summary>
        /// <param key="word">The word to consider.</param>
        public static explicit operator string(BdoScriptword word)
        {
            return word?.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="st"></param>
        /// <param key="word"></param>
        /// <returns></returns>
        public static string operator +(string st, BdoScriptword word)
            => st + "{{" + word?.ToString() + "}}";

        /// <summary>
        /// 
        /// </summary>
        /// <param key="st"></param>
        /// <param key="word"></param>
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
        /// Sub script words of this instance.
        /// </summary>
        public IBdoScriptword Child { get; set; }

        // Parameters ----------------------------------

        /// <summary>
        /// Returns a string that represents this instance.
        /// </summary>
        /// <returns>Retuns the string that represents this instance.</returns>
        public override string Key() => Id;

        /// <summary>
        /// Get the root script word of this instance.
        /// </summary>
        /// <returns>The root script word of this instance.</returns>
        public IBdoScriptword Last(int levelMax = 50)
        {
            return levelMax > 0 ? Child == null ? this : Child.Last(levelMax--) : null;
        }

        /// <summary>
        /// Returns a string that represents this instance.
        /// </summary>
        /// <returns>Retuns the string that represents this instance.</returns>
        public override string ToString() => ToString(true);

        public string ToString(bool lastChild)
        {
            IBdoScriptword current = this;
            if (lastChild && current.Parent == null)
            {
                current = current.LastChild();
            }

            string script;
            switch (current.Kind)
            {
                case ScriptItemKinds.Function:
                    script = string.Join(", ", current.Select(p => p.ToString(DataValueTypes.Any, true)).ToArray());
                    script = (current.Parent == null ? BdoScriptHelper.Symbol_Fun : "")
                        + current.Name + "(" + script + ")";
                    if (current.Parent is BdoScriptword subFunScriptWord)
                    {
                        script = subFunScriptWord?.ToString(false) + "." + script;
                    }
                    return script;
                case ScriptItemKinds.Variable:
                    script = (current.Parent == null ? BdoScriptHelper.Symbol_Fun : "")
                        + "('" + current.Name?.Replace("'", "''") + "')";
                    if (current.Parent is BdoScriptword subVarScriptWord)
                    {
                        script = subVarScriptWord?.ToString(false) + "." + script;
                    }
                    return script;
                case ScriptItemKinds.None:
                    return string.Empty;
                case ScriptItemKinds.Text:
                case ScriptItemKinds.Syntax:
                case ScriptItemKinds.Literal:
                case ScriptItemKinds.Any:
                default:
                    return Name;
            }
        }

        #endregion

        // --------------------------------------------------
        // IClonable Implementation
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param key="areas">The areas to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone()
        {
            return MemberwiseClone();
        }

        #endregion

        // --------------------------------------------------
        // IDisposable Implementation
        // --------------------------------------------------

        #region IDisposable Implementation

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes specifying whether this instance is disposing.
        /// </summary>
        /// <param key="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;
        }

        #endregion

    }
}
