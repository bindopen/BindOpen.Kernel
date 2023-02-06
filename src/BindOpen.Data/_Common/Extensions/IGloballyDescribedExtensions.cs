﻿using System.Collections.Generic;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents an described data item.
    /// </summary>
    public static class IGloballyDescribedExtensions
    {
        public static T AddDescription<T>(
            this T obj,
            KeyValuePair<string, string> item)
            where T : IGloballyDescribed
        {
            if (obj != null)
            {
                obj.Description ??= BdoData.NewDictionary();
                obj.Description.Add(item);
            }

            return obj;
        }
    }
}
