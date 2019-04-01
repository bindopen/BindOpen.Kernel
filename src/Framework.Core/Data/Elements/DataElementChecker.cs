using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Extensions.Configuration.Routines;
using BindOpen.Framework.Core.Extensions.Definition.Routines;
using BindOpen.Framework.Core.Extensions.Runtime.Routines;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Specification.Constraints
{
    /// <summary>
    /// This static class provides methods to check data elements.
    /// </summary>
    public static class DataElementChecker
    {
        /// <summary>
        /// Check value.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="dataElement">The element to consider.</param>
        /// <param name="isDeepCheck">Indicates whether other rules than allowed and forbidden values are checked.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>The log of check log.</returns>
        public static ILog CheckItem(
            object item,
            IDataElement dataElement,
            bool isDeepCheck = false,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet =null)
        {
            ILog log = new Log();

            if (appScope != null && appScope.AppExtension !=null)
                if (!isDeepCheck)
                {
                    Routine routine_AllowedValues =
                        appScope.CreateItem<RoutineDefinition>(null, this.GetConstraint("AllowedValues"), null, log) as Routine;
                    Routine routine_ForbiddenValues =
                        appScope.CreateItem<RoutineDefinition>(null, this.GetConstraint("ForbiddenValues"), null, log) as Routine;

                    if (routine_AllowedValues != null)
                        log.AddEvents(routine_AllowedValues.Execute(appScope, scriptVariableSet, item, dataElement));
                    if (routine_ForbiddenValues != null)
                        log.AddEvents(routine_ForbiddenValues.Execute(appScope, scriptVariableSet, item, dataElement));
                }
                else
                    foreach (RoutineConfiguration routineConfiguration in this.Items)
                    {
                        Routine routine =
                            appScope.CreateItem<RoutineDefinition>(null, routineConfiguration, null, log) as Routine;

                        if (routine != null)
                            log.AddEvents(routine.Execute(appScope, scriptVariableSet, item, dataElement));
                    }

            return log;
        }

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="dataElement">The element to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>The log of check log.</returns>
        public virtual ILog CheckItem(
            object item,
            IDataElement dataElement = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            ILog log = new Log();
            if (item != null)
            {
                if (_constraintStatement != null)
                {
                    log = _constraintStatement.CheckItem(item, dataElement, true, appScope, scriptVariableSet);
                }
            }

            return log;
        }

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="dataElement">The element to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>The log of check log.</returns>
        public virtual ILog CheckElement(
            IDataElement dataElement,
            string[] specificationAreas = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            ILog log = new Log();

            if (dataElement == null)
                return log;

            if (specificationAreas == null)
                specificationAreas = new[] { nameof(DataAreaKind.Any) };

            if (specificationAreas.Contains(nameof(DataAreaKind.Any)) || specificationAreas.Contains("element"))
            {
                if (!_availableItemizationModes.Contains(DataItemizationMode.Any) && !_availableItemizationModes.Contains(dataElement.ItemizationMode))
                {
                    log.AddError(
                        title: "Itemization mode not available",
                        description: "The itemization mode of this element is not available.");
                }
                else
                {
                    switch (dataElement.ItemizationMode)
                    {
                        case DataItemizationMode.Referenced:
                            if (dataElement.ItemReference == null)
                            {
                                log.AddWarning(
                                   title: "Item reference missing in element",
                                   description: "This element has no item reference where as it is in reference itemization mode.");
                            }

                            switch (GetAreaSpecification("item").RequirementLevel)
                            {
                                case RequirementLevel.OptionalExclusively:
                                    if ((string.IsNullOrEmpty(dataElement.ItemScript)) || (dataElement.Items.Count > 0))
                                        log.AddError(
                                            title: "Item script and items forbidden with reference",
                                            description: "No item reference.");
                                    break;
                                case RequirementLevel.Forbidden:
                                    if (dataElement.ItemReference != null)
                                        log.AddWarning(
                                            title: "Item reference forbidden",
                                            description: "No item reference.");
                                    break;
                                case RequirementLevel.Required:
                                    if (dataElement.ItemReference == null)
                                        log.AddError(
                                            title: "Item reference required",
                                            description: "The element requires a item reference.");
                                    break;
                            }
                            break;
                        case DataItemizationMode.Script:
                            if (string.IsNullOrEmpty(dataElement.ItemScript))
                                log.AddWarning(
                                    title: "Item script missing in element",
                                    description: "The element has no item script where as it is in script itemization mode.");

                            switch (GetAreaSpecification("item").RequirementLevel)
                            {
                                case RequirementLevel.OptionalExclusively:
                                    if ((string.IsNullOrEmpty(dataElement.ItemScript)) || (dataElement.Items.Count > 0))
                                        log.AddError(
                                            title: "Item script and items forbidden forbidden with reference",
                                            description: "No item reference.");
                                    break;
                                case RequirementLevel.Forbidden:
                                    if (dataElement.ItemReference != null)
                                        log.AddWarning(
                                            title: "Item reference forbidden",
                                            description: "No item reference.");
                                    break;
                                case RequirementLevel.Required:
                                    if (dataElement.ItemReference == null)
                                        log.AddError(
                                            title: "Item reference required",
                                            description: "The element requires a item reference.");
                                    break;
                            }
                            break;
                        case DataItemizationMode.Valued:
                            if ((!IsValueList) && (dataElement.Items.Count > 1))
                                log.AddWarning(
                                    title: "More than one item found in element",
                                    description: "The element has more than one item where as it is in single itemization mode.");

                            switch (GetAreaSpecification("item").RequirementLevel)
                            {
                                case RequirementLevel.OptionalExclusively:
                                    if ((string.IsNullOrEmpty(dataElement.ItemScript)) || (dataElement.Items.Count > 0))
                                        log.AddError(
                                            title: "Item script and items forbidden forbidden with reference",
                                            description: "No item reference.");
                                    break;
                                case RequirementLevel.Forbidden:
                                    if (dataElement.ItemReference != null)
                                        log.AddWarning(
                                            title: "Item specification allows reference forbidden",
                                            description: "No item reference.");
                                    break;
                                case RequirementLevel.Required:
                                    if ((dataElement.Items == null) || (dataElement.Items.Count == 0))
                                        log.AddError(
                                            title: "Items required",
                                            description: "The element requires items.");
                                    break;
                            }
                            break;
                    }
                }
            }

            if (specificationAreas.Contains(nameof(DataAreaKind.Any)) || specificationAreas.Contains(nameof(DataAreaKind.Items)))
            {
                if (IsValueList)
                {
                    if (MinimumItemNumber > dataElement.Items.Count)
                        log.AddError(
                            title: "Not enough items in element",
                            description: "The element has " + dataElement.Items.Count + " items where as the minimum was specified at " + MinimumItemNumber + ".");

                    if (MaximumItemNumber > -1 && MaximumItemNumber < dataElement.Items.Count)
                        log.AddError(
                            title: "Too many items in element",
                            description: "The element has " + dataElement.Items.Count + " items where as the maximum was specified at " + MaximumItemNumber + ".");
                }

                foreach (object item in dataElement.Items)
                    log.AddEvents(CheckItem(aItem, dataElement, appScope, scriptVariableSet));
            }

            return log;
        }

    }
}
