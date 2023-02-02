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
        /// <remarks>Put reference sets as null if you do not want to repair this instance.</remarks>
        public static void Update(
            this BdoMetaSet metaSet,
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
        /// <remarks>Put reference sets as null if you do not want to repair this instance.</remarks>
        public static void Update(
            this BdoMetaSet metaSet,
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
            this BdoMetaSet metaSet,
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
            this BdoMetaSet metaSet,
            IBdoMetaSet refElementSet = null,
            UpdateModes[] updateModes = null,
            string[] specAreas = null,
            IBdoLog log = null)
        {
            if (specAreas == null)
                specAreas = new[] { nameof(DataAreaKind.Any) };

            if (updateModes == null)
                updateModes = new[] { UpdateModes.Full };

            var items = metaSet?.Items;
            var refItems = refElementSet?.Items;

            if (refElementSet != null)
            {
                // we check that all the elems in this instance are in the specified item

                if (updateModes.Has(UpdateModes.Incremental_RemoveMissingInSource)
                    || updateModes.Has(UpdateModes.Incremental_UpdateCommon))
                {
                    int i = 0;

                    if (refItems != null)
                    {
                        while (i < items.Count)
                        {
                            var currentSubItem = items[i];

                            var referenceSubItem = refItems.Find(p => p.BdoKeyEquals(currentSubItem));
                            if (referenceSubItem == null)
                            {
                                if (updateModes.Has(UpdateModes.Incremental_RemoveMissingInSource))
                                {
                                    items.RemoveAt(i);
                                    i--;
                                }
                            }
                            else if (updateModes.Has(UpdateModes.Incremental_UpdateCommon))
                            {
                                //referenceSubItem.Repair(referenceSubItem, specAreas, log: log);
                            }

                            i++;
                        }
                    }
                }

                // we check that all the elems in specified item are in this instance

                if (updateModes.Has(UpdateModes.Incremental_AddMissingInTarget))
                {
                    if (refItems != null)
                    {
                        foreach (var referenceSubItem in refItems)
                        {
                            var currentSubItem = items.Find(p => p.BdoKeyEquals(referenceSubItem));

                            //if (currentSubItem == null)
                            //    Add(ElementFactory.CreateFromSpec(referenceSubItem) as BdoElement);
                        }
                    }
                }
            }
        }
    }
}
