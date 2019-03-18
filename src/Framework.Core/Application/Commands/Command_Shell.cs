using System;
using System.Diagnostics;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Application.Commands
{
    /// <summary>
    /// This class represents the Shell command.
    /// </summary>
    [Serializable()]
    [XmlType("ShellCommand", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "command", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class ShellCommand : Command
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The file name of this instance.
        /// </summary>
        [XmlElement("fileName")]
        public String FileName { get; set; } = "";

        /// <summary>
        /// The argument String of this instance.
        /// </summary>
        [XmlElement("argumentString")]
        public string ArgumentString { get; set; } = "";

        /// <summary>
        /// The working directory of this instance.
        /// </summary>
        [XmlElement("workingDirectory")]
        public String WorkingDirectory { get; set; } = "";

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
        /// <param name="aArgumentString">The argument string to consider.</param>
        /// <param name="aWorkingDirectory">The working directory to consider.</param>
        /// <param name="name">The name of this instance.</param>
        public ShellCommand(
            String fileName,
            String aArgumentString = null,
            String aWorkingDirectory = null,
            String name = null)
            : base(CommandKind.Shell, name)
        {
            this.FileName = fileName;
            this.ArgumentString = aArgumentString;
            this.WorkingDirectory = aWorkingDirectory;
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
        public override Log ExecuteWithResult(
            out String resultString,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            RuntimeMode runtimeMode = RuntimeMode.Normal)
        {
            resultString = "";

            Log log = appScope.Check(true);
            if (String.IsNullOrEmpty(this.FileName))
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
                    //aProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
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
        public override Object Clone()
        {
            return base.Clone() as ShellCommand;
        }

        #endregion

    }
}
