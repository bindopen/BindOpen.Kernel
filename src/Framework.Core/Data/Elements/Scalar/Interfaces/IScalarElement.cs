using BindOpen.Framework.Core.Data.Common;

namespace BindOpen.Framework.Core.Data.Elements.Scalar
{
    public interface IScalarElement : IDataElement
    {
        object Value { get; set; }

        DataValueType ValueType { get; set; }

        new ScalarElementSpec Specification { get; set; }
    }
}