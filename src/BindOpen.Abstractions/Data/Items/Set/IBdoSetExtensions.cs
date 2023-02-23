namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class IBdoSetExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static T Descendant<T>(
            this IBdoSet list,
            params string[] names)
            where T : class, IReferenced
        {
            if (list != null)
            {
                IReferenced current = list;
                foreach (string name in names)
                {
                    if (current is IBdoSet currentList)
                    {
                        current = currentList[name];
                        if (current == null) break;
                    }
                }

                return current as T;
            }

            return default;
        }
    }
}
