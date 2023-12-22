namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents a group.
    /// </summary>
    public interface ITGroupsOf<T> : ITBdoSet<T>
        where T : IReferenced, IGrouped
    {
        /// <summary>
        /// Removes the specified child.
        /// </summary>
        /// <param name="groupId">The group identifier to consider.</param>
        /// <param name="isRecursive">Indicates whether the search is recursive.</param>
        void RemoveOfGroup(string groupId, bool isRecursive = false);
    }
}