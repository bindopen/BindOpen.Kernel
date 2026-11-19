using BindOpen.Logging;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines an updatable object by specifying the class of the object used for update.
    /// </summary>
    public interface IUpdatable
    {
        /// <summary>
        /// Updates this object with the specified item.
        /// </summary>
        /// <param key="item">The item to consider.</param>
        /// <param key="areas">The areas of update.</param>
        /// <param key="updateModes">The update modes to consider.</param>
        /// <param key="log">The BindOpen log used for tracking.</param>
        /// <returns></returns>
        void Update(
            object item,
            string[] areas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null);
    }
}