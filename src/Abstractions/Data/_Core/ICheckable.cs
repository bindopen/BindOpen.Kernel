using BindOpen.Logging;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents an object that be checked.
    /// </summary>
    public interface ICheckable
    {
        /// <summary>
        /// Cheks this object.
        /// </summary>
        /// <param key="isExistenceChecked">Indicates whether the existence of the object must be checked.</param>
        /// <param key="areas">The areas of checking.</param>
        /// <param key="log">The BindOpen log used for tracking.</param>
        /// <returns></returns>
        void Check(bool isExistenceChecked = true, string[] areas = null, IBdoLog log = null);
    }
}