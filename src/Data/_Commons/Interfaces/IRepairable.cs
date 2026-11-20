using BindOpen.Logging;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines a reparaible object.
    /// </summary>
    public interface IRepairable
    {
        /// <summary>
        /// Repais this object.
        /// </summary>
        /// <param key="areas">The areas of reperation.</param>
        /// <param key="updateModes">The update modes to consider.</param>
        /// <param key="log">The BindOpen log used for tracking.</param>
        /// <returns></returns>
        void Repair(string[] areas = null, UpdateModes[] updateModes = null, IBdoLog log = null);
    }
}