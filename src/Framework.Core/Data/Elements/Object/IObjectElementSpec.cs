using BindOpen.Framework.Core.Data.Specification.Filters;

namespace BindOpen.Framework.Core.Data.Elements._Object
{
    public interface IObjectElementSpec : IDataElementSpec
    {
        IDataValueFilter ClassFilter { get; set; }
    }
}