using BindOpen.Framework.Data.Items;

namespace BindOpen.Framework.Runtime.Application.Languages
{
    /// <summary>
    /// This interface defines an 
    /// application language.
    /// </summary>
    public interface IApplicationLanguage : IDescribedDataItem
    {
        /// <summary>
        /// The culture name.
        /// </summary>
        string CultureName { get; set; }

        /// <summary>
        /// The UI culture name.
        /// </summary>
        string UICultureName { get; set; }
    }
}