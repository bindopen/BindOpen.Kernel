using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions.Items.Tasks;
using BindOpen.Framework.Core.Extensions.Items.Tasks.Definition;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Items.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITask : ITAppExtensionItem<ITaskDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="appScope"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="log"></param>
        /// <param name="taskEntryKinds"></param>
        /// <returns></returns>
        object GetEntryObjectWithName(
            string name,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null,
            params TaskEntryKind[] taskEntryKinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataElementSpecSet"></param>
        /// <param name="taskEntryKind"></param>
        /// <returns></returns>
        bool IsCompatibleWith(IDataElementSpecSet dataElementSpecSet, TaskEntryKind taskEntryKind = TaskEntryKind.Any);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specificationLevel"></param>
        /// <returns></returns>
        bool IsConfigurable(SpecificationLevels specificationLevel = SpecificationLevels.Runtime);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relativePath"></param>
        void UpdateAbsolutePaths(string relativePath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="appScope"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="runtimeMode"></param>
        void Execute(
            ILog log,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            RuntimeMode runtimeMode = RuntimeMode.Normal);
    }
}