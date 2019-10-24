using BindOpen.Framework.Core.Data.Elements.Scalar;

namespace BindOpen.Framework.Runtime.Application.Options
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