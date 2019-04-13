using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;

namespace BindOpen.Framework.Core.Data.Elements.Scalar
{
    public interface IScalarElementSpec : IDataElementSpec
    {
        DataValueType ValueType { get; set; }
    }
}