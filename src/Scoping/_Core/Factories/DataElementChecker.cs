﻿namespace BindOpen.Scoping
{
    /// <summary>
    /// This static class provides methods to check data elements.
    /// </summary>
    public static class BdoElementChecker
    {
        //// --------------------------------------------------
        //// CHECKING
        //// --------------------------------------------------

        //#region Checking

        ///// <summary>
        ///// Check the specified item.
        ///// </summary>
        ///// <param key="item">The item to consider.</param>
        ///// <param key="dataElement">The element to consider.</param>
        ///// <returns>The log of check log.</returns>
        //public static IBdoLog CheckItem(
        //    object item,
        //    IBdoElement dataElement = null)
        //{
        //    var log = new BdoLog();
        //    if (item != null && dataElement != null)
        //    {
        //        if (dataElement.Schemaification?.SchemaRuleStatement != null)
        //        {
        //            log.AddEvents(_ruleStatement.CheckItem(item, dataElement, true));
        //        }
        //    }

        //    return log;
        //}

        ///// <summary>
        ///// Check the specified item.
        ///// </summary>
        ///// <param key="dataElement">The element to consider.</param>
        ///// <param key="schemaAreas">The schema areas to consider.</param>
        ///// <returns>The log of check log.</returns>
        //public virtual IBdoLog CheckElement(
        //    IBdoElement dataElement,
        //    string[] schemaAreas = null)
        //{
        //    var log = new BdoLog();

        //    if (dataElement == null)
        //        return log;

        //    if (schemaAreas == null)
        //        schemaAreas = new[] { nameof(DataAreaKind.Any) };

        //    if (schemaAreas.Contains(nameof(DataAreaKind.Any)) || schemaAreas.Contains("element"))
        //    {
        //        if (!_availableValueModes.Contains(DataValueMode.Any) && !_availableValueModes.Contains(dataElement.ValueMode))
        //        {
        //            log.AddError(
        //                title: "Itemization mode not available",
        //                description: "The itemization mode of this element is not available.");
        //        }
        //        else
        //        {
        //            switch (dataElement.ValueMode)
        //            {
        //                case DataValueMode.Referenced:
        //                    if (dataElement.ItemReference == null)
        //                    {
        //                        log.AddWarning(
        //                           title: "Item reference missing in element",
        //                           description: "This element has no item reference where as it is in reference itemization mode.");
        //                    }

        //                    switch (GetAreaSpecification("item").RequirementLevel)
        //                    {
        //                        case RequirementLevels.OptionalExclusively:
        //                            if ((string.IsNullOrEmpty(dataElement.ItemScript)) || (dataElement.Items.Count > 0))
        //                                log.AddError(
        //                                    title: "Item script and items forbidden with reference",
        //                                    description: "No item reference.");
        //                            break;
        //                        case RequirementLevels.Forbidden:
        //                            if (dataElement.ItemReference != null)
        //                                log.AddWarning(
        //                                    title: "Item reference forbidden",
        //                                    description: "No item reference.");
        //                            break;
        //                        case RequirementLevels.Required:
        //                            if (dataElement.ItemReference == null)
        //                                log.AddError(
        //                                    title: "Item reference required",
        //                                    description: "The element requires a item reference.");
        //                            break;
        //                    }
        //                    break;
        //                case DataValueMode.Script:
        //                    if (string.IsNullOrEmpty(dataElement.ItemScript))
        //                        log.AddWarning(
        //                            title: "Item script missing in element",
        //                            description: "The element has no item script where as it is in script itemization mode.");

        //                    switch (GetAreaSpecification("item").RequirementLevel)
        //                    {
        //                        case RequirementLevels.OptionalExclusively:
        //                            if ((string.IsNullOrEmpty(dataElement.ItemScript)) || (dataElement.Items.Count > 0))
        //                                log.AddError(
        //                                    title: "Item script and items forbidden forbidden with reference",
        //                                    description: "No item reference.");
        //                            break;
        //                        case RequirementLevels.Forbidden:
        //                            if (dataElement.ItemReference != null)
        //                                log.AddWarning(
        //                                    title: "Item reference forbidden",
        //                                    description: "No item reference.");
        //                            break;
        //                        case RequirementLevels.Required:
        //                            if (dataElement.ItemReference == null)
        //                                log.AddError(
        //                                    title: "Item reference required",
        //                                    description: "The element requires a item reference.");
        //                            break;
        //                    }
        //                    break;
        //                case DataValueMode.Valued:
        //                    if ((!IsValueList) && (dataElement.Items.Count > 1))
        //                        log.AddWarning(
        //                            title: "More than one item found in element",
        //                            description: "The element has more than one item where as it is in single itemization mode.");

        //                    switch (GetAreaSpecification("item").RequirementLevel)
        //                    {
        //                        case RequirementLevels.OptionalExclusively:
        //                            if ((string.IsNullOrEmpty(dataElement.ItemScript)) || (dataElement.Items.Count > 0))
        //                                log.AddError(
        //                                    title: "Item script and items forbidden forbidden with reference",
        //                                    description: "No item reference.");
        //                            break;
        //                        case RequirementLevels.Forbidden:
        //                            if (dataElement.ItemReference != null)
        //                                log.AddWarning(
        //                                    title: "Item schema allows reference forbidden",
        //                                    description: "No item reference.");
        //                            break;
        //                        case RequirementLevels.Required:
        //                            if ((dataElement.Items == null) || (dataElement.Items.Count == 0))
        //                                log.AddError(
        //                                    title: "Items required",
        //                                    description: "The element requires items.");
        //                            break;
        //                    }
        //                    break;
        //            }
        //        }
        //    }

        //    if (schemaAreas.Contains(nameof(DataAreaKind.Any)) || schemaAreas.Contains(nameof(DataAreaKind.Items)))
        //    {
        //        if (IsValueList)
        //        {
        //            if (MinDataItemNumber > dataElement.Items.Count)
        //                log.AddError(
        //                    title: "Not enough items in element",
        //                    description: "The element has " + dataElement.Items.Count + " items where as the minimum was specified at " + MinDataItemNumber + ".");

        //            if (MaxDataItemNumber > -1 && MaxDataItemNumber < dataElement.Items.Count)
        //                log.AddError(
        //                    title: "Too many items in element",
        //                    description: "The element has " + dataElement.Items.Count + " items where as the maximum was specified at " + MaxDataItemNumber + ".");
        //        }

        //        foreach (object item in dataElement.Items)
        //            log.AddEvents(CheckItem(item, dataElement));
        //    }

        //    return log;
        //}

        //#endregion
    }
}
