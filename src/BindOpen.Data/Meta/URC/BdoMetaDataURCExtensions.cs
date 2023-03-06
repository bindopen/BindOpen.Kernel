using BindOpen.Data.Meta;
using BindOpen.Logging;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class BdoMetaDataURCExtensions
    {
        // Update

        public static IBdoMetaData Update(
            this IBdoMetaData item,
            IBdoMetaData refItem = default,
            UpdateModes[] updateModes = null,
            string[] specAreas = null,
            IBdoLog log = null)
        {
            specAreas ??= new[] { nameof(DataAreaKind.Any) };

            updateModes ??= new[] { UpdateModes.Full };

            if (item != null && refItem != null)
            {
                //var items = list?.Items;
                //var refItems = refList?.Items;

                //if (updateModes.Has(UpdateModes.Full))
                //{
                //    list.With(refItems?.ToArray());
                //}
                //else
                //{
                //    // we check that all the elems in this instance are in the specified item

                //    if (updateModes.Has(
                //            UpdateModes.Incremental_RemoveMissingInSource,
                //            UpdateModes.Incremental_UpdateCommon))
                //    {
                //        int i = 0;

                //        while (i < items.Count)
                //        {
                //            var item = items[i];

                //            var refItem = refList[item?.Name];
                //            if (refItem == null)
                //            {
                //                if (updateModes.Has(UpdateModes.Incremental_RemoveMissingInSource))
                //                {
                //                    items.RemoveAt(i);
                //                    i--;
                //                }
                //            }
                //            else if (updateModes.Has(UpdateModes.Incremental_UpdateCommon))
                //            {
                //                item.Update(refItem, updateModes, specAreas, log);
                //            }

                //            i++;
                //        }
                //    }

                //    // we check that all the elems in specified item are in this instance

                //    if (updateModes.Has(UpdateModes.Incremental_AddMissingInTarget))
                //    {
                //        if (refItems != null)
                //        {
                //            foreach (var referenceSubItem in refItems)
                //            {
                //                var currentSubItem = items.Find(p => p.BdoKeyEquals(referenceSubItem));

                //                //if (currentSubItem == null)
                //                //    Add(ElementFactory.CreateFromSpec(referenceSubItem) as BdoElement);
                //            }
                //        }
                //    }
                //}
            }

            return item;
        }

        // Check

        public static bool Check(
            this IBdoMetaData item,
            IBdoMetaData refItem = default,
            string[] specAreas = null,
            bool isExistenceChecked = true,
            IBdoLog log = null)
        {
            return true;
        }

        // Repair

        public static void Repair(
            this IBdoMetaData item,
            IBdoSpec spec = null,
            UpdateModes[] updateModes = null,
            string[] specAreas = null,
            IBdoLog log = null)
        {
        }
    }
}