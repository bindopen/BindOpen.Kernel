using BindOpen.Data.Specification;
using BindOpen.Logging;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static class BdoMetaSetExtension
    {
        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public static void Update(
            this IBdoMetaSet elemSet,
            IBdoMetaSet refElementSet = null,
            UpdateModes[] updateModes = null,
            string[] specAreas = null,
            IBdoLog log = null)
        {
            if (specAreas == null)
                specAreas = new[] { nameof(DataAreaKind.Any) };

            if (updateModes == null)
                updateModes = new[] { UpdateModes.Incremental_AddMissingInTarget };
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public static void Update(
            this IBdoMetaSet elemSet,
            IBdoMetaSpecSet refElementSpecSet = null,
            UpdateModes[] updateModes = null,
            string[] specAreas = null,
            IBdoLog log = null)
        {
            if (specAreas == null)
                specAreas = new[] { nameof(DataAreaKind.Any) };

            if (updateModes == null)
                updateModes = new[] { UpdateModes.Incremental_AddMissingInTarget };
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="isExistenceChecked">Indicates whether the entity existence is checked.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="specAreas">The specification areas to consider.</param>
        /// <returns>Returns the check log.</returns>
        public static void Check(
            this IBdoMetaSet elemSet,
            IBdoMetaSet refElementSet = null,
            string[] specAreas = null,
            bool isExistenceChecked = true,
            IBdoLog log = null)
        {
            if (specAreas == null)
                specAreas = new[] { nameof(DataAreaKind.Any) };
        }

        /// <summary>
        /// Repairs this instance with the specified definition.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <returns>Log of the operation.</returns>
        public static void Repair(
            this IBdoMetaSet elemSet,
            IBdoMetaSet refElementSet = null,
            UpdateModes[] updateModes = null,
            string[] specAreas = null,
            IBdoLog log = null)
        {
            if (specAreas == null)
                specAreas = new[] { nameof(DataAreaKind.Any) };

            if (updateModes == null)
                updateModes = new[] { UpdateModes.Full };

            if (refElementSet != null)
            {
                // we check that all the elems in this instance are in the specified item

                if (updateModes.Has(UpdateModes.Incremental_RemoveMissingInSource)
                    || updateModes.Has(UpdateModes.Incremental_UpdateCommon))
                {
                    int i = 0;

                    if (refElementSet.Items != null)
                    {
                        while (i < elemSet.Items.Count)
                        {
                            var currentSubItem = elemSet.Items[i];

                            var referenceSubItem = refElementSet.Items.Find(p => p.BdoKeyEquals(currentSubItem));
                            if (referenceSubItem == null)
                            {
                                if (updateModes.Has(UpdateModes.Incremental_RemoveMissingInSource))
                                {
                                    elemSet.Items.RemoveAt(i);
                                    i--;
                                }
                            }
                            else if (updateModes.Has(UpdateModes.Incremental_UpdateCommon))
                            {
                                referenceSubItem.Repair(referenceSubItem, specAreas, log: log);
                            }

                            i++;
                        }
                    }
                }

                // we check that all the elems in specified item are in this instance

                if (updateModes.Has(UpdateModes.Incremental_AddMissingInTarget))
                {
                    if (refElementSet.Items != null)
                    {
                        foreach (var referenceSubItem in refElementSet.Items)
                        {
                            var currentSubItem = elemSet.Items.Find(p => p.BdoKeyEquals(referenceSubItem));

                            //if (currentSubItem == null)
                            //    Add(ElementFactory.CreateFromSpec(referenceSubItem) as BdoElement);
                        }
                    }
                }
            }
        }
    }
}
