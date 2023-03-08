using BindOpen.Data.Helpers;
using BindOpen.Logging;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class BdoMetaSetURCExtensions
    {
        // Update

        public static IBdoMetaSet Update(
            this IBdoMetaSet list,
            IBdoMetaSet refList = default,
            UpdateModes[] updateModes = null,
            string[] specAreas = null,
            IBdoLog log = null)
        {
            specAreas ??= new[] { nameof(DataAreaKind.Any) };

            updateModes ??= new[] { UpdateModes.Full };

            if (list != null && refList != null)
            {
                var items = list?.Items;
                var refItems = refList?.Items;

                if (updateModes.Has(UpdateModes.Full))
                {
                    list.With(refItems?.ToArray());
                }
                else
                {
                    // we check that all the elems in this instance are in the specified item

                    if (updateModes.Has(
                            UpdateModes.Incremental_RemoveMissingInSource,
                            UpdateModes.Incremental_UpdateCommon))
                    {
                        int i = 0;

                        while (i < items.Count)
                        {
                            var item = items[i];

                            var refItem = refList[item?.Name];
                            if (refItem == null)
                            {
                                if (updateModes.Has(UpdateModes.Incremental_RemoveMissingInSource))
                                {
                                    items.RemoveAt(i);
                                    i--;
                                }
                            }
                            else if (updateModes.Has(UpdateModes.Incremental_UpdateCommon))
                            {
                                item.Update(refItem, updateModes, specAreas, log);
                            }

                            i++;
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

            return list;
        }

        // Check

        public static bool Check(
            this IBdoMetaSet list,
            IBdoMetaSet refList = default,
            string[] specAreas = null,
            bool isExistenceChecked = true,
            IBdoLog log = null)
        {
            return true;
        }

        // Repair

        public static void Repair(
            this IBdoMetaSet list,
            IBdoSpec spec = null,
            UpdateModes[] updateModes = null,
            string[] specAreas = null,
            IBdoLog log = null)
        {
        }
    }
}