using System;

namespace BindOpen.Scoping.Data.Helpers
{
    /// <summary>
    /// This class represents a common helper.
    /// </summary>
    public static class CommonHelper
    {
        /// <summary>
        /// Executes the specified action if the specified value is null or empry.
        /// </summary>
        /// <param key="value">The value to consider.</param>
        /// <param key="action">The action to consider.</param>
        public static void IsNullOrEmpty(string value, Action action)
        {
            if (string.IsNullOrEmpty(value))
            {
                action?.Invoke();
            }
        }

        /// <summary>
        /// Executes the specified action if the specified value is true.
        /// </summary>
        /// <param key="value">The value to consider.</param>
        /// <param key="action">The action to consider.</param>
        public static void If(bool value, Action action)
        {
            if (value)
            {
                action?.Invoke();
            }
        }
    }
}
