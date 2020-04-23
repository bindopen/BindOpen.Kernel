using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Items;
using BindOpen.Extensions.Definition;
using System.Collections.Generic;

namespace BindOpen.System.Scripting
{
    /// <summary>
    /// This class represents a script variable box that allows to store interpretation variables.
    /// </summary>
    /// <remarks>Interpreation variables are variables that cannot be evaluated directly though definitions. Example current objects.</remarks>
    public class ScriptVariableSet : DataItem, IScriptVariableSet
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
            return (variableName != null) && (this._variables.ContainsKey(variableName.ToKey()));
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
        public IScriptVariableSet SetValue(StoredDataItem item)
        {
            if (item != null)
            {
                switch (item.GetType().GetExtensionItemKind())
                {
                    case BdoExtensionItemKind.Task:
                        return SetValue("currentTask", item);
                    default:
                        break;
                }
            }

            if (item is DataElement)
            {
                return SetValue("currentElement", item);
            }

            if (item is DataItem)
            {
                return SetValue("currentItem", item);
            }

            return this;
        }

        /// <summary>
        /// Adds the specified named data item.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="value">The value to consider.</param>
        public IScriptVariableSet SetValue(string name, object value)
        {
            if (this.Has(name))
                this._variables.Remove(name.ToKey());

            this._variables.Add(name.ToKey(), value);

            return this;
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
            ScriptVariableSet scriptVariableSet = base.Clone(areas) as ScriptVariableSet;
            scriptVariableSet._variables = this._variables;
            return scriptVariableSet;
        }

        #endregion
    }
}
