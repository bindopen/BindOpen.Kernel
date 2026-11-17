namespace BindOpen.Data
{
    /// <summary>
    /// This static class extends IActivable interface.
    /// </summary>
    public static class ITSingleChildParentExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        public static Q WithChild<Q, T>(this Q item, T child)
            where Q : ITSingleChildParent<T>
            where T : IReferenced
        {
            if (item != null)
            {
                item.Child = child;
            }

            return item;
        }
    }
}
