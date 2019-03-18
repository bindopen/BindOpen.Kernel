using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Application.Options
{
    /// <summary>
    /// This class represents a option set.
    /// </summary>
    [XmlType("OptionSet", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot("optionSet", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class OptionSet : DataElementSet
    {

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the OptionSet class.
        /// </summary>
        public OptionSet()
            : base()
        {
        }

        #endregion


        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Indicates whether this instance has the specified option.
        /// </summary>
        /// <param name="name">Name of the option to consider.</param>
        public Boolean HasOption(String name)
        {
            return this.HasItem(name);
        }

        /// <summary>
        /// Gets the value of the specified option.
        /// </summary>
        /// <param name="name">Name of the option to consider.</param>
        public Object GetOptionValue(String name)
        {
            return this.GetElementItemObject(name);
        }

        /// <summary>
        /// Gets the string value of the specified option.
        /// </summary>
        /// <param name="name">Name of the option to consider.</param>
        public String GetOptionStringValue(String name)
        {
            return (this.GetElementItemObject(name) as String ?? "");
        }

        #endregion


        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Updates this instance with the specified string value.
        /// </summary>
        /// <param name="stringValue">The string value to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        public Log Update(
            String stringValue,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();

            if (!String.IsNullOrEmpty(stringValue))
                foreach (String optionString in stringValue.Split(';'))
                    if (optionString.Contains("="))
                    {
                        int index = optionString.IndexOf("=");
                        String optionName = optionString.Substring(0, index);
                        String optionValue = optionString.Substring(index + 1);
                        this.AddElementItem(optionName, optionValue, appScope, scriptVariableSet, log);
                    }

            return log;
        }

        #endregion

    }
}
