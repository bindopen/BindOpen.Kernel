using System;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// This enumeration lists the possible modes of parameter display.
    /// </summary>
    [Flags]
    public enum DbQueryParameterDisplayModes
    {
        /// <summary>
        /// Mode when parameters are scripted.
        /// </summary>
        Scripted = 0x01,

        /// <summary>
        /// Mode when values of parameters are injected.
        /// </summary>
        ValueInjected = 0x02,

        /// <summary>
        /// Mode when parameters are simply showed.
        /// </summary>
        SimplyShowed = 0x04,
    }

}
