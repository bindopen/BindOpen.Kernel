using BindOpen.Framework.Data.Elements;

namespace BindOpen.Framework.Runtime.Application.Options
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOptionSet : IDataElementSet
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        object GetOptionValue(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool HasOption(string name);
    }
}