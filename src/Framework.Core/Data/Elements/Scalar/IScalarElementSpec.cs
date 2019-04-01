using BindOpen.Framework.Core.Data.Common;

namespace BindOpen.Framework.Core.Data.Elements.Scalar
{
    public interface IScalarElementSpec : IDataElementSpec
    {
        DataValueType ValueType { get; set; }
    }
}