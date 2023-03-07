using BindOpen.Data;
using BindOpen.Data.Items;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This class represents a BindOpen extension runtime item.
    /// </summary>
    public interface IBdoExtension : IBdoItem, IIdentified
    {
        string DefinitionUniqueName { get; set; }
    }
}

