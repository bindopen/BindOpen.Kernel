using BindOpen.Framework.Data.Elements;

namespace BindOpen.Framework.Application.Options
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOptionSpec : IScalarElementSpec
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="argumentstring"></param>
        /// <returns></returns>
        bool IsArgumentMarching(string argumentstring);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="argumentstring"></param>
        /// <param name="aliasIndex"></param>
        /// <returns></returns>
        bool IsArgumentMarching(string argumentstring, out int aliasIndex);
    }
}