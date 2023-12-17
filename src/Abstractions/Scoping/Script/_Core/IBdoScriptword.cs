using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Meta;

namespace BindOpen.Kernel.Scoping.Script
{
    /// <summary>
    /// This interface represents a script word.
    /// </summary>
    public interface IBdoScriptword : IBdoMetaObject, IBdoObjectNotMetable, ITSingleChildParent<IBdoScriptword>
    {
        /// <summary>
        /// The kind of this instance.
        /// </summary>
        ScriptItemKinds Kind { get; set; }

        /// <summary>
        /// Gets the last script word of this instance considering the specified maximum level.
        /// </summary>
        /// <param name="levelMax">The maximum level to consider.</param>
        /// <returns>Returns the last script word of this instance.</returns>
        IBdoScriptword Last(int levelMax = 50);
    }
}