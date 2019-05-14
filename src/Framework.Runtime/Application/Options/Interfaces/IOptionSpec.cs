using BindOpen.Framework.Core.Data.Elements.Scalar;

namespace BindOpen.Framework.Runtime.Application.Options
{
    public interface IOptionSpec : IScalarElementSpec
    {
        bool IsArgumentMarching(string argumentstring);
        bool IsArgumentMarching(string argumentstring, out int aliasIndex);
    }
}