using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Extensions.Configuration.Tasks;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Application.Commands
{

    /// <summary>
    /// This class represents a command.
    /// </summary>
    [Serializable()]
    [XmlType("Command", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "command", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    [XmlInclude(typeof(ShellCommand))]
    [XmlInclude(typeof(ReferenceCommand))]
    [XmlInclude(typeof(ScriptCommand))]
    public abstract class Command : TaskConfiguration
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        [XmlElement("kind")]
        public CommandKind Kind { get; set; } = CommandKind.None;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Command class.
        /// </summary>
        protected Command()
            : this(CommandKind.None)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Command class.
        /// </summary>
        /// <param name="kind">The kind of command to consider.</param>
        protected Command(CommandKind kind)
            : this(kind, null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Command class.
        /// </summary>
        /// <param name="kind">The kidn of command to consider.</param>
        /// <param name="name">The name of this instance.</param>
        protected Command(CommandKind kind, String name = null) : base(name,null, null, "command_")
        {
            this.Kind = kind;
        }

        #endregion

        //------------------------------------------
        // EXECUTION
        //-----------------------------------------

        #region Execution

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="runtimeMode">The runtime mode to consider.</param>
        /// <returns>The log of execution log.</returns>
        public void Execute(
            Log log,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            RuntimeMode runtimeMode = RuntimeMode.Normal)
        {
            log = (log ?? new Log());
            log.Append(this.ExecuteWithResult(out string resultString, appScope, scriptVariableSet, runtimeMode));
        }

        /// <summary>
        /// Executes this instance with result.
        /// </summary>
        /// <param name="resultString">The result to get.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="runtimeMode">The runtime mode to consider.</param>
        /// <returns>The log of execution log.</returns>
        public virtual Log ExecuteWithResult(
            out String resultString,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            RuntimeMode runtimeMode = RuntimeMode.Normal)
        {
            resultString = "";
            return new Log();
        }

        #endregion

    }
}
