using BindOpen.Data.Helpers;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents an described data item.
    /// </summary>
    public static class ITChildExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static T Root<T>(
            this ITChild<T> obj,
            int levelMax = 50)
            where T : IReferenced
        {
            if (obj != null && levelMax > 0)
            {
                if (obj.Parent == null)
                {
                    return obj.As<T>();
                }
                else if (obj.Parent is ITChild<T> tree)
                {
                    return tree.Root(levelMax--);
                }
            }

            return default;
        }
    }
}
