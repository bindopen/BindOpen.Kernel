using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using System.Linq;

namespace BindOpen.Scoping.Script
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
            ExpressionKind = BdoExpressionKind.Word;
            this.WithDataType(DataValueTypes.Scriptword);
        }

        #endregion

        // -----------------------------------------------
        // Converters
        // -----------------------------------------------

        #region Converters

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

        // ------------------------------------------
        // IBdoExpression Implementation
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The value of this instance.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        public BdoExpressionKind ExpressionKind { get; set; } = BdoExpressionKind.Auto;

        #endregion

        // -----------------------------------------------
        // IBdoScriptword Implementation
        // -----------------------------------------------

        #region IBdoScriptword

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        /// <example>Script word, syntax, text...</example>
        public ScriptTokenKinds TokenKind { get; set; } = ScriptTokenKinds.None;

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
        public override string Key() => Identifier;

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
            switch (current.TokenKind)
            {
                case ScriptTokenKinds.Function:
                    script = string.Join(", ", current.Select(p => p.ToString(DataValueTypes.Any, true)).ToArray());
                    script = (current.Parent == null ? BdoScriptHelper.Symbol_Fun : "")
                        + current.Name + "(" + script + ")";
                    if (current.Parent is BdoScriptword subFunScriptWord)
                    {
                        script = subFunScriptWord?.ToString(false) + "." + script;
                    }
                    return script;
                case ScriptTokenKinds.Variable:
                    script = (current.Parent == null ? BdoScriptHelper.Symbol_Fun : "")
                        + "('" + current.Name?.Replace("'", "''") + "')";
                    if (current.Parent is BdoScriptword subVarScriptWord)
                    {
                        script = subVarScriptWord?.ToString(false) + "." + script;
                    }
                    return script;
                case ScriptTokenKinds.None:
                    return string.Empty;
                case ScriptTokenKinds.Text:
                case ScriptTokenKinds.Syntax:
                case ScriptTokenKinds.Literal:
                case ScriptTokenKinds.Any:
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
