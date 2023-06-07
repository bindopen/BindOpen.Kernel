using BindOpen.Scoping.Data;

namespace BindOpen.Scoping.Extensions
{
    /// <summary>
    /// This class represents a BindOpen extension runtime item.
    /// </summary>
    public interface IBdoExtension :
        IBdoObject, IIdentified, IBdoDefinable
    {
    }
}

