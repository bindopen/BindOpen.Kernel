using BindOpen.System.Logging;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface represents an updatable object by specifying the class of the object used for update.
    /// </summary>
    public interface ITUpdatable<T>
    {
        /// <summary>
        /// Updates this object.
        /// </summary>
        /// <param key="areas">The areas of update.</param>
        /// <param key="updateModes">The update modes to consider.</param>
        /// <param key="log">The BindOpen log used for tracking.</param>
        /// <returns></returns>
        void Update(
            T refItem,
            string[] areas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null);
    }
}