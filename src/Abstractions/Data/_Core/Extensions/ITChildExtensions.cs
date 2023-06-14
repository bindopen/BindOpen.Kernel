using BindOpen.System.Data.Helpers;

namespace BindOpen.System.Data
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

        /// <summary>
        /// The level of this instance.
        /// </summary>
        public static int Level<T>(this ITChild<T> child)
            where T : IReferenced
        {
            if (child != null)
            {
                var parent = child.Parent;
                if (parent is ITChild<T> parentChild)
                {
                    return parentChild.Level() + 1;
                }
            }

            return 0;
        }
    }
}
