using BindOpen.Logging;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data 
    /// </summary>
    public static partial class IBdoMetaDataExtensions
    {
        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <returns>ILog of the operation.</returns>
        /// <remarks>Put reference sets as null if you do not want to repair this instance.</remarks>
        public static void Update(
            this IBdoMetaData el,
            IBdoMetaData refEl = null,
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
                || (specificationAreas.Contains(nameof(BdoMetaDataAreaKind.Element))))
            {
                el.WithName(refEl.Name);
                el.WithIndex(refEl.Index);
            }

            if ((specificationAreas.Contains(nameof(DataAreaKind.Any)))
                || (specificationAreas.Contains(nameof(DataAreaKind.Items))))
            {
                if (el.DataValueType == DataValueTypes.Any)
                {
                    el.WithDataValueType(refEl.DataValueType);
                }

                el.WithDataExpression(refEl.DataExpression);
                el.WithDataReference(refEl.DataReference);

                el.WithDataList(refEl.GetData(log: log));
            }

            if ((specificationAreas.Contains(nameof(DataAreaKind.Any)))
                || (specificationAreas.Contains(nameof(DataAreaKind.Properties))))
            {
            }
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="isExistenceChecked">Indicates whether the entity existence is checked.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <returns>Returns the check log.</returns>
        public static void Check(
            this IBdoMetaData el,
            IBdoMetaData refEl = null,
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
            this IBdoMetaData el,
            IBdoMetaData refEl = null,
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
                    (specificationAreas.Contains(nameof(BdoMetaDataAreaKind.Element))))
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
                        if (el.DataValueType == DataValueTypes.None || el.DataValueType == DataValueTypes.Any)
                        {
                            el.WithDataValueType(refEl.DataValueType);
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
