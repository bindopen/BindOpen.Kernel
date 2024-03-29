﻿namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines an described data item.
    /// </summary>
    public static class ITitledExtensions
    {
        public static T WithTitle<T>(
            this T obj,
            string text)
            where T : ITitled
        {
            if (obj != null)
            {
                obj.Title = text;
            }

            return obj;
        }
    }
}
