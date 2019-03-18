using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Common;
using System;
using System.Collections;

namespace BindOpen.Framework.Core.System.Scripting
{
    /// <summary>
    /// This class represents a script variable box that allows to store interpretation variables.
    /// </summary>
    /// <remarks>Interpreation variables are variables that cannot be evaluated directly though definitions. Example current objects.</remarks>
    public class ScriptVariableSet : DataItem
    {

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private Hashtable _Variables = new Hashtable();

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
        public Object GetValue(String variableName)
        {
            String key = variableName.ToKey();
            return (this._Variables.ContainsKey(key) ? this._Variables[key] : null);
        }

        /// <summary>
        /// Indicates whether this instance has the specified variable.
        /// </summary>
        /// <param name="variableName">The name of the variable to consider.</param>
        /// <returns>Returns True if this instance has the specified variable.</returns>
        public Boolean Has(String variableName)
        {
            return (variableName!=null) && (this._Variables.ContainsKey(variableName.ToKey()));
        }

        #endregion


        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Adds the specified named data item.
        /// </summary>
        /// <param name="storedDataItem">The named data item to consider.</param>
        /// <returns>Returns true if the specified item has been added.</returns>
        public Boolean SetValue(StoredDataItem storedDataItem)
        {
            DictionaryEntry aAddedEntry = new DictionaryEntry();
            if (storedDataItem!=null)
                switch (storedDataItem.GetType().GetExtensionItemKind())
                {
                    case AppExtensionItemKind.Task:
                        aAddedEntry = this.SetValue("currentTask", storedDataItem);
                        break;
                }

            if (storedDataItem is DataElement)
                aAddedEntry = this.SetValue("currentElement", storedDataItem);

            if (storedDataItem is DataItem)
                aAddedEntry = this.SetValue("currentItem", storedDataItem);

            return aAddedEntry.Key != null;
        }

        /// <summary>
        /// Adds the specified named data item.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="value">The value to consider.</param>
        public DictionaryEntry SetValue(String name, Object value)
        {
            if (this.Has(name))
                this._Variables.Remove(name.ToKey());
            
            this._Variables.Add(name.ToKey(), value);
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
        public override Object Clone()
        {
            ScriptVariableSet scriptVariableSet = base.Clone() as ScriptVariableSet;
            scriptVariableSet._Variables = this._Variables;
            return scriptVariableSet;
        }

        #endregion
    
    }
}
