namespace BindOpen.Scoping.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoSetExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static T Descendant<T>(
            this IBdoSet list,
            params object[] tokens)
            where T : class, IReferenced
        {
            if (list != null)
            {
                IReferenced current = list;
                foreach (object token in tokens)
                {
                    if (current is IBdoSet currentList)
                    {
                        if (token is string key)
                        {
                            current = currentList[key];
                            if (current == null) break;
                        }
                        else if (token is int index)
                        {
                            current = currentList[index];
                            if (current == null) break;
                        }
                    }
                }

                return current as T;
            }

            return default;
        }
    }
}