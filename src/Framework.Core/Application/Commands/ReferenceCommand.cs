using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.References;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Application.Commands
{

    /// <summary>
    /// This class represents the data reference command.
    /// </summary>
    [Serializable()]
    [XmlType("ReferenceCommand", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "command", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class ReferenceCommand : Command, IReferenceCommand
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The task set of this instance.
        /// </summary>
        [XmlElement("reference")]
        public IDataReference Reference { get; set; } = new DataReference();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ReferenceCommand class.
        /// </summary>
        public ReferenceCommand() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ReferenceCommand class.
        /// </summary>
        /// <param name="reference">The reference to consider.</param>
        /// <param name="name">The name of this instance.</param>
        public ReferenceCommand(
            DataReference reference, String name = null)
            : base(CommandKind.Reference, name)
        {
            this.Reference = reference;
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

            Log log = appScope.Check(true);
            if (this.Reference==null)
            {
                log.AddWarning(
                    title: "Reference missing",
                    description: "No reference defined in command '" + this.Key() + "'.");
            }
            else if (!log.HasErrorsOrExceptions()&& this.Reference != null)
            {
                scriptVariableSet.SetValue("currentItem", this.Reference.SourceElement.GetItem());
                scriptVariableSet.SetValue("currentElement", this.Reference.SourceElement);
                resultString = this.Reference.Get(appScope, scriptVariableSet, log)?.ToString();
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
            ReferenceCommand aReferenceCommand = base.Clone() as ReferenceCommand;
            if (this.Reference!=null)
                aReferenceCommand.Reference = this.Reference.Clone() as DataReference;
            return aReferenceCommand;
        }

        #endregion
    }
}
