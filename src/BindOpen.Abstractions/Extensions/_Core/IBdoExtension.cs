using BindOpen.Data;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This class represents a BindOpen extension runtime item.
    /// </summary>
    public interface IBdoExtension :
        IBdoItem, IIdentified, IBdoDefinable
    {
    }
}

