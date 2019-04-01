using BindOpen.Framework.Core.Data.Common;

namespace BindOpen.Framework.Core.Data.Elements.Scalar
{
    public interface IScalarElement : IDataElement
    {
        string Value { get; set; }

        DataValueType ValueType { get; set; }

        new IScalarElementSpec Specification { get; set; }
    }
}