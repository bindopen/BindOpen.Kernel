using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Runtime.Application.Navigation;

namespace BindOpen.Framework.Runtime.Application.Rights
{

    /// <summary>
    /// This structure respresents an user permission.
    /// </summary>
    [Serializable()]
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
                base.Key = value?.ToLower().Trim() ?? "";
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
                base.Content = value?.ToString().ToLower() ?? "";
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
            this.ActionName = actionName;
            this.ValueScript = valueScript;
        }

        /// <summary>
        /// Creates a new instance of the UserPermission class.
        /// </summary>
        /// <param name="actionKind">The action kind to consider.</param>
        /// <param name="valueScript">The value script to consider.</param>
        public UserPermission(ActionKind actionKind, String valueScript) : this(actionKind.ToString(), valueScript)
        {
        }

        /// <summary>
        /// Creates a new instance of the UserPermission class.
        /// </summary>
        /// <param name="actionName">The action name to consider.</param>
        /// <param name="value">The boolean value to consider.</param>
        public UserPermission(String actionName, Boolean? value)
        {
            this.ActionName = actionName;
            this.Value = value;
        }

        /// <summary>
        /// Creates a new instance of the UserPermission class.
        /// </summary>
        /// <param name="actionKind">The action kind to consider.</param>
        /// <param name="value">The boolean value to consider.</param>
        public UserPermission(ActionKind actionKind, Boolean? value) : this(actionKind.ToString(), value)
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
        public Boolean GetValue(ScriptInterpreter scriptInterpreter = null, ScriptVariableSet scriptVariableSet = null)
        {
            String value = this.ValueScript;

            if (value != null)
                if (value.ToLower().Trim() == "%true()")
                    return true;
                else if (value.ToLower().Trim() == "%false()")
                    return false;
            
            if (scriptInterpreter!=null)
            {
                scriptInterpreter.Evaluate(this.ValueScript, out value, scriptVariableSet);
                return ((value != null) && (value.ToLower().Trim() == "%true()"));
            }

            return false;
        }

        #endregion

    }
}