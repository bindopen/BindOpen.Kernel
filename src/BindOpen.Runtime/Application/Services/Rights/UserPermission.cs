using BindOpen.Application.Navigation;
using BindOpen.Data.Expression;
using BindOpen.Data.Items;
using BindOpen.System.Scripting;
using System;
using System.Xml.Serialization;

namespace BindOpen.Application.Rights
{

    /// <summary>
    /// This structure respresents an user permission.
    /// </summary>
    public class UserPermission : DataKeyValue
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Action name of this instance.
        /// </summary>
        [XmlAttribute("action")]
        public string ActionName
        {
            set
            {
                base.Key = value?.ToLower().Trim() ?? string.Empty;
            }
            get
            {
                return base.Key;
            }
        }

        /// <summary>
        /// The value of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean? Value
        {
            set
            {
                base.Content = value?.ToString().ToLower() ?? string.Empty;
            }
            get
            {
                return base.Content?.ToLower() == "true";
            }
        }

        /// <summary>
        /// The value of this instance.
        /// </summary>
        [XmlAttribute("value")]
        public string ValueScript
        {
            set
            {
                base.Content = value;
            }
            get
            {
                return base.Content;
            }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Creates a new instance of the UserPermission class.
        /// </summary>
        public UserPermission()
        {
        }

        /// <summary>
        /// Creates a new instance of the UserPermission class.
        /// </summary>
        /// <param name="actionName">The action name to consider.</param>
        /// <param name="valueScript">The value script to consider.</param>
        public UserPermission(String actionName, String valueScript)
        {
            ActionName = actionName;
            ValueScript = valueScript;
        }

        /// <summary>
        /// Creates a new instance of the UserPermission class.
        /// </summary>
        /// <param name="actionKind">The action kind to consider.</param>
        /// <param name="valueScript">The value script to consider.</param>
        public UserPermission(ActionKinds actionKind, String valueScript) : this(actionKind.ToString(), valueScript)
        {
        }

        /// <summary>
        /// Creates a new instance of the UserPermission class.
        /// </summary>
        /// <param name="actionName">The action name to consider.</param>
        /// <param name="value">The boolean value to consider.</param>
        public UserPermission(String actionName, Boolean? value)
        {
            ActionName = actionName;
            Value = value;
        }

        /// <summary>
        /// Creates a new instance of the UserPermission class.
        /// </summary>
        /// <param name="actionKind">The action kind to consider.</param>
        /// <param name="value">The boolean value to consider.</param>
        public UserPermission(ActionKinds actionKind, Boolean? value) : this(actionKind.ToString(), value)
        {
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the value of this instance.
        /// </summary>
        /// <param name="scriptInterpreter">The script interpreter to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <returns>Returns the value of this instance.</returns>
        public bool GetValue(IBdoScriptInterpreter scriptInterpreter = null, IScriptVariableSet scriptVariableSet = null)
        {
            string valueString = ValueScript?.Trim();

            if (valueString?.Trim().Equals("true", StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }

            if (scriptInterpreter != null)
            {
                return scriptInterpreter.Evaluate(valueString, DataExpressionKind.Script, scriptVariableSet) as bool? == true;
            }

            return false;
        }

        #endregion

    }
}