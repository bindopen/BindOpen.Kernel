using BindOpen.Framework.Core.Data.Elements;

namespace BindOpen.Framework.Core.Data.Elements._Object
{
    public interface IObjectElement : IDataElement
    {
        string ClassFullName { get; set; }

        new ObjectElementSpec Specification { get; set; }
    }
}