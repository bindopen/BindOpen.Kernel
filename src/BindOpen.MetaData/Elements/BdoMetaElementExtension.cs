using BindOpen.Logging;
using BindOpen.MetaData.Specification;
using System.Linq;

namespace BindOpen.MetaData.Elements
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
            this IBdoMetaElement el,
            IBdoMetaElement refEl = null,
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
                el.WithName(refEl.Name);
                el.WithTitle(refEl.Title);
                el.WithDescription(refEl.Description);
                el.WithIndex(refEl.Index);
            }

            if ((specificationAreas.Contains(nameof(DataAreaKind.Any)))
                || (specificationAreas.Contains(nameof(DataAreaKind.Items))))
            {
                if (el.ValueType == DataValueTypes.Any)
                {
                    el.WithValueType(refEl.ValueType);
                }

                el.WithItemScript(refEl.ItemScript);
                el.WithItemReference(refEl.ItemReference);

                el.WithItems(refEl.Item(log: log));
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
            this IBdoMetaElement el,
            IBdoMetaElement refEl = null,
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
            //            log.AddEvents(Specification?.ConstraintStatement.co(subItem, el, true));
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
            this IBdoMetaElement el,
            IBdoMetaElement refEl = null,
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null)
        {
            if (el != null)
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
                    if (refEl != null)
                    {
                        //if (el.Specification != null)
                        //    if (el.Specification.AvailableItemizationModes.Count == 1)
                        //        el.WithItemizationMode(refEl.Specification.AvailableItemizationModes[0]);
                    }
                }

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) ||
                    (specificationAreas.Contains(nameof(DataAreaKind.Items))))
                {
                    if (refEl != null)
                    {
                        if (el.ValueType == DataValueTypes.None || el.ValueType == DataValueTypes.Any)
                        {
                            el.WithValueType(refEl.ValueType);
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
