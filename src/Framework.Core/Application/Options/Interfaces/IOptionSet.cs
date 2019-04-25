using BindOpen.Framework.Core.Data.Elements.Sets;

namespace BindOpen.Framework.Core.Application.Options
{
    public interface IOptionSet : IDataElementSet
    {
        object GetOptionValue(string name);
        bool HasOption(string name);
    }
}