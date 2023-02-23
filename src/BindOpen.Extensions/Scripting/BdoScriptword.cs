using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Data.Items;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Extensions.Scripting
{
    /// <summary>
    /// This class represents a script word.
    /// </summary>
    public class BdoScriptword : BdoItem,
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

        // -----------------------------------------------
        // IBdoScriptword Implementation
        // -----------------------------------------------

        #region IBdoScriptword

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        /// <example>Script word, syntax, text...</example>
        public string DefinitionUniqueName { get; set; }

        /// <summary>
        /// The name of this instance.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The name of this instance.
        /// </summary>
        public string Id { get; set; }

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

        /// <summary>
        /// Sub script words of this instance.
        /// </summary>
        public IBdoScriptword Parent { get; set; }

        // Item ----------------------------------

        /// <summary>
        /// The item of this instance that is the result of interpretation.
        /// </summary>
        public object Data { get; set; }

        // Parameters ----------------------------------

        /// <summary>
        /// Returns a string that represents this instance.
        /// </summary>
        /// <returns>Retuns the string that represents this instance.</returns>
        public string Key() => Id;

        /// <summary>
        /// Get the root script word of this instance.
        /// </summary>
        /// <returns>The root script word of this instance.</returns>
        public IBdoScriptword Root(int levelMax = 50)
        {
            return levelMax > 0 ? (Parent == null ? this : Parent.Root(levelMax--)) : null;
        }

        /// <summary>
        /// Get the root script word of this instance.
        /// </summary>
        /// <returns>The root script word of this instance.</returns>
        public IBdoScriptword Last(int levelMax = 50)
        {
            return levelMax > 0 ? (Child == null ? this : Child.Last(levelMax--)) : null;
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
                    script = string.Join(", ", this.Select(p => p.ToString(DataValueTypes.Any, true)).ToArray());
                    script = (showSymbol ? BdoScriptHelper.Symbol_Fun : "")
                        + Name + "(" + script + ")";
                    if (Child is BdoScriptword subFunScriptWord)
                    {
                        script += "." + subFunScriptWord?.ToString(false);
                    }
                    return script;
                case ScriptItemKinds.Variable:
                    script = (showSymbol ? BdoScriptHelper.Symbol_Fun : "")
                        + "('" + Name?.Replace("'", "''") + "')";
                    if (Child is BdoScriptword subVarScriptWord)
                    {
                        script += "." + subVarScriptWord?.ToString(false);
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

        // ------------------------------------------
        // ITDataList Implementation
        // ------------------------------------------

        #region ITDataList

        private List<object> _params = new();

        /// <summary>
        /// Returns the number of items.
        /// </summary>
        public int Count
        {
            get => _params?.Count ?? 0;
        }

        public object this[int index]
        {
            get => _params.GetAt(index);
            set
            {
                if (index > 0 && index < Count) _params[index] = value;
            }
        }

        public bool IsReadOnly
        {
            get => false;
        }

        public int IndexOf(object item)
        {
            return _params?.IndexOf(item) ?? -1;
        }

        public void Insert(int index, object item)
        {
            _params?.Insert(index, item);
        }


        public void RemoveAt(int index)
        {
            _params?.RemoveAt(index);
        }

        public void Add(object item)
        {
            _params?.Add(item);
        }

        public void Clear()
        {
            _params?.Clear();
        }

        public bool Contains(object item)
        {
            return _params?.Contains(item) ?? false;
        }

        public void CopyTo(object[] array, int arrayIndex)
        {
            _params?.CopyTo(array, arrayIndex);
        }

        public bool Remove(object item)
        {
            return _params?.Remove(item) ?? false;
        }

        public IEnumerator<object> GetEnumerator() => _params.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _params.GetEnumerator();

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
        public override object Clone(params string[] areas)
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
