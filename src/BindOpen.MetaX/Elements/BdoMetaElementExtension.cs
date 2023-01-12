using BindOpen.Logging;
using BindOpen.Meta.Specification;
using System.Linq;

namespace BindOpen.Meta.Elements
{
    /// <summary>
    /// This class represents a data 
    /// </summary>
    public static class BdoMetaElementExtension
    {
        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <returns>ILog of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public static void Update(
            this IBdoMetaElement element,
            IBdoMetaElement refElement = null,
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null)
        {
            if (specificationAreas == null)
                specificationAreas = new[] { nameof(DataAreaKind.Any) };

            if ((specificationAreas.Contains(nameof(DataAreaKind.Any)))
                || (specificationAreas.Contains(nameof(DataAreaKind.Constraints))))
            {
            }

            if ((specificationAreas.Contains(nameof(DataAreaKind.Any)))
                || (specificationAreas.Contains(nameof(DataAreaKind.Design))))
            {
            }

            if ((specificationAreas.Contains(nameof(DataAreaKind.Any)))
                || (specificationAreas.Contains(nameof(BdoMetaElementAreaKind.Element))))
            {
                element.WithName(refElement.Name);
                element.WithTitle(refElement.Title);
                element.WithDescription(refElement.Description);
                element.WithIndex(refElement.Index);
            }

            if ((specificationAreas.Contains(nameof(DataAreaKind.Any)))
                || (specificationAreas.Contains(nameof(DataAreaKind.Items))))
            {
                if (element.ValueType == DataValueTypes.Any)
                {
                    element.WithValueType(refElement.ValueType);
                }

                element.WithItemScript(refElement.ItemScript);
                element.WithItemReference(refElement.ItemReference);

                element.WithItem(refElement.GetItem(log: log));
            }

            if ((specificationAreas.Contains(nameof(DataAreaKind.Any)))
                || (specificationAreas.Contains(nameof(DataAreaKind.Properties))))
            {
            }
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="isExistenceChecked">Indicates whether the carrier existence is checked.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <returns>Returns the check log.</returns>
        public static void Check(
            this IBdoMetaElement element,
            IBdoMetaElement refElement = null,
            string[] specificationAreas = null,
            bool isExistenceChecked = true,
            IBdoLog log = null)
        {
            if (specificationAreas == null)
                specificationAreas = new[] { nameof(DataAreaKind.Any) };

            if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) ||
                (specificationAreas.Contains(nameof(DataAreaKind.Constraints))))
            {
            }

            if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) ||
                (specificationAreas.Contains(nameof(DataAreaKind.Design))))
            {
            }

            //if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) ||
            //    (specificationAreas.Contains(nameof(DataAreaKind.Items))))
            //    if (Specification != null)
            //    {
            //        foreach (object subItem in Items)
            //        {
            //            log.AddEvents(Specification?.ConstraintStatement.co(subItem, element, true));
            //        }
            //    }

            if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) ||
                (specificationAreas.Contains(nameof(DataAreaKind.Properties))))
            {
            }
        }

        /// <summary>
        /// Repairs this instance with the specified definition.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <returns>ILog of the operation.</returns>
        public static void Repair(
            this IBdoMetaElement element,
            IBdoMetaElement refElement = null,
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null)
        {
            if (element != null)
            {
                if (specificationAreas == null)
                    specificationAreas = new[] { nameof(DataAreaKind.Any) };

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any)))
                    || (specificationAreas.Contains(nameof(DataAreaKind.Constraints))))
                {
                }

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) ||
                    (specificationAreas.Contains(nameof(DataAreaKind.Design))))
                {
                }

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) ||
                    (specificationAreas.Contains(nameof(BdoMetaElementAreaKind.Element))))
                {
                    if (refElement != null)
                    {
                        //if (element.Specification != null)
                        //    if (element.Specification.AvailableItemizationModes.Count == 1)
                        //        element.WithItemizationMode(refElement.Specification.AvailableItemizationModes[0]);
                    }
                }

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) ||
                    (specificationAreas.Contains(nameof(DataAreaKind.Items))))
                {
                    if (refElement != null)
                    {
                        if (element.ValueType == DataValueTypes.None || element.ValueType == DataValueTypes.Any)
                        {
                            element.WithValueType(refElement.ValueType);
                        }
                    }
                }

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) ||
                    (specificationAreas.Contains(nameof(DataAreaKind.Properties))))
                {
                }
            }
        }
    }
}
