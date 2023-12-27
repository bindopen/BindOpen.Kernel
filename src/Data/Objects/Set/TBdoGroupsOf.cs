namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data item set.
    /// </summary>
    /// <typeparam name="T">The class of the named data items.</typeparam>
    public partial class TBdoGroupsOf<T> : TBdoSet<T>, ITBdoGroupsOf<T>
        where T : IReferenced, IGrouped
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TBdoGroupsOf class.
        /// </summary>
        public TBdoGroupsOf() : base()
        {
        }

        #endregion

        // ------------------------------------------
        // IGroup Implementation
        // ------------------------------------------

        #region IGroup

        public void RemoveOfGroup(string groupId, bool isRecursive = false)
        {
            Remove(q => q.OfGroup(groupId));

            if (isRecursive)
            {
                foreach (var child in this)
                {
                    if (child is ITBdoGroupsOf<T> specChild)
                    {
                        specChild.RemoveOfGroup(groupId, true);
                    }
                }
            }
        }

        #endregion
    }
}
