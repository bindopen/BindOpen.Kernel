using System;
using System.Diagnostics;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Commands.Interfaces;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Application.Scopes.Interfaces;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.System.Diagnostics.Interfaces;
using BindOpen.Framework.Core.System.Scripting.Interfaces;

namespace BindOpen.Framework.Core.Application.Commands
{
    /// <summary>
    /// This class represents the Shell command.
    /// </summary>
    [Serializable()]
    [XmlType("ShellCommand", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "command", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class ShellCommand : Command, IShellCommand
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The file name of this instance.
        /// </summary>
        [XmlElement("fileName")]
        public string FileName { get; set; } = "";

        /// <summary>
        /// The argument string of this instance.
        /// </summary>
        [XmlElement("argumentString")]
        public string ArgumentString { get; set; } = "";

        /// <summary>
        /// The working directory of this instance.
        /// </summary>
        [XmlElement("workingDirectory")]
        public string WorkingDirectory { get; set; } = "";

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ShellCommand class.
        /// </summary>
        public ShellCommand() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ShellCommand class.
        /// </summary>
        /// <param name="fileName">The file name to consider.</param>
        /// <param name="argumentString">The argument string to consider.</param>
        /// <param name="workingDirectory">The working directory to consider.</param>
        /// <param name="name">The name of this instance.</param>
        public ShellCommand(
            string fileName,
            string argumentString = null,
            string workingDirectory = null,
            string name = null)
            : base(CommandKind.Shell, name)
        {
            this.FileName = fileName;
            this.ArgumentString = argumentString;
            this.WorkingDirectory = workingDirectory;
        }

        #endregion

        // ------------------------------------------
        // EXECUTING
        // ------------------------------------------

        #region Executing

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

            ILog log = appScope.Check(true);

            if (string.IsNullOrEmpty(this.FileName))
            {
                log.AddWarning(
                    title: "File name missing",
                    description: "No file name defined in command '" + this.Key() + "'.");
            }
            else if (!log.HasErrorsOrExceptions())
            {
                try
                {
                    Process process = new Process();
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.FileName = this.FileName;
                    process.StartInfo.Arguments = this.ArgumentString;
                    process.StartInfo.WorkingDirectory = this.WorkingDirectory;
                    process.Start();
                    resultString = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                }
                catch (Exception ex)
                {
                    log.AddException(ex);
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
            return base.Clone() as ShellCommand;
        }

        #endregion
    }
}
