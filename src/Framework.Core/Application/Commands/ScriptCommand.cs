using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Application.Commands
{
    /// <summary>
    /// This class represents the script command.
    /// </summary>
    [Serializable()]
    [XmlType("ScriptCommand", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "command", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class ScriptCommand : Command, IScriptCommand
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private string _script = null;

        #endregion

        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The script of this instance.
        /// </summary>
        [XmlElement("script")]
        public string Script
        {
            get { return this._script ?? ""; }
            set { this._script = value; }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScriptCommand class.
        /// </summary>
        public ScriptCommand() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ScriptCommand class.
        /// </summary>
        /// <param name="script">The script to consider.</param>
        /// <param name="name">The name of this instance.</param>
        public ScriptCommand(
            string script, string name = null)
            : base(CommandKind.Script, name)
        {
            this._script = script;
        }

        #endregion

        //------------------------------------------
        // EXECUTION
        //-----------------------------------------

        #region Execution

        /// <summary>
        /// Executes this instance with result.
        /// </summary>
        /// <param name="resultString">The result to get.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="runtimeMode">The runtime mode to consider.</param>
        /// <returns>The log of execution log.</returns>
        public override ILog ExecuteWithResult(
            out string resultString,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            RuntimeMode runtimeMode = RuntimeMode.Normal)
        {
            resultString = "";

            Log log = appScope.Check(false, true);
            if (!log.HasErrorsOrExceptions())
            {
                if (string.IsNullOrEmpty(this._script))
                {
                    log.AddWarning(
                        title: "Script missing",
                        description: "No script defined in command '" + this.Key() + "'.");
                }
                else
                {
                    appScope.ScriptInterpreter.Evaluate(this._script, out resultString, scriptVariableSet, log);
                }
            }

            return log;
        }

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone()
        {
            return base.Clone() as ScriptCommand;
        }

        #endregion
    }
}
