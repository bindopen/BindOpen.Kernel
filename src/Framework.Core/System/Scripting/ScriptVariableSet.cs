using System.Collections;
using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Definition.Items;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.System.Scripting
{
    /// <summary>
    /// This class represents a script variable box that allows to store interpretation variables.
    /// </summary>
    /// <remarks>Interpreation variables are variables that cannot be evaluated directly though definitions. Example current objects.</remarks>
    public class ScriptVariableSet : DataItem, IBdoScriptVariableSet
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private Dictionary<string, object> _variables = new Dictionary<string, object>();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScriptVariableSet class.
        /// </summary>
        public ScriptVariableSet()
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the value of the specified variable.
        /// </summary>
        /// <param name="variableName">The name of the variable to consider.</param>
        /// <returns>Returns the value of the specified variable.</returns>
        public object GetValue(string variableName)
        {
            string key = variableName.ToKey();
            return this._variables.ContainsKey(key) ? this._variables[key] : null;
        }

        /// <summary>
        /// Indicates whether this instance has the specified variable.
        /// </summary>
        /// <param name="variableName">The name of the variable to consider.</param>
        /// <returns>Returns True if this instance has the specified variable.</returns>
        public bool Has(string variableName)
        {
            return (variableName!=null) && (this._variables.ContainsKey(variableName.ToKey()));
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Adds the specified named data item.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <returns>Returns true if the specified item has been added.</returns>
        public bool SetValue(StoredDataItem item)
        {
            DictionaryEntry entry = new DictionaryEntry();
            if (item!=null)
            {
                switch (item.GetType().GetExtensionItemKind())
                {
                    case BdoExtensionItemKind.Task:
                        entry = this.SetValue("currentTask", item);
                        break;
                    default:
                        break;
                }
            }

            if (item is DataElement)
                entry = this.SetValue("currentElement", item);

            if (item is DataItem)
                entry = this.SetValue("currentItem", item);

            return entry.Key != null;
        }

        /// <summary>
        /// Adds the specified named data item.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="value">The value to consider.</param>
        public DictionaryEntry SetValue(string name, object value)
        {
            if (this.Has(name))
                this._variables.Remove(name.ToKey());
            
            this._variables.Add(name.ToKey(), value);
            return new DictionaryEntry(name.ToKey(), value);
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
            ScriptVariableSet scriptVariableSet = base.Clone() as ScriptVariableSet;
            scriptVariableSet._variables = this._variables;
            return scriptVariableSet;
        }

        #endregion
    }
}
