using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System;
using System.Linq;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a meta data.
    /// </summary>
    public static partial class BdoMetaDataExtensions
    {
        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static IBdoMetaData ToMetaData(
            this object obj,
            string name = null,
            IBdoScope scope = null,
            IBdoLog log = null)
        {
            var meta = BdoMeta.New(name, obj);
            if (meta is IBdoMetaObject metaObj)
            {
                metaObj.With(
                    obj.ToMetaArray(
                        metaObj.GetClassType(scope, log)));
            }

            return meta;
        }

        /// <summary>
        /// Creates a data element set from a dynamic object.
        /// </summary>
        /// <param name="obj">The objet to consider.</param>
        public static IBdoMetaData[] ToMetaArray(
            this object obj,
            Type type = null,
            bool onlyMetaAttributes = true)
            => obj.ToMetaList(type, onlyMetaAttributes)?.ToArray();

        /// <summary>
        /// Indicates whether this instance is compatible with the specified item.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <returns></returns>
        public static bool IsCompatibleWithData<T>(
            this T spec,
            object item)
            where T : IBdoSpec
        {
            return spec != null
                && (spec.ValueType == DataValueTypes.Any
                || item.GetValueType().IsCompatibleWith(spec.ValueType));
        }

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
                if (el.ValueType == DataValueTypes.Any)
                {
                    el.WithDataValueType(refEl.ValueType);
                }

                el.WithDataExpression(refEl.Expression);
                el.WithDataReference(refEl.Reference);

                //el.WithData(refEl.GetData(log: log));
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
                        if (el.ValueType == DataValueTypes.None || el.ValueType == DataValueTypes.Any)
                        {
                            el.WithDataValueType(refEl.ValueType);
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
