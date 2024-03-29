﻿using BindOpen.Data.Meta;
using BindOpen.Logging;

namespace BindOpen.Scoping.Script
{
    /// <summary>
    /// This class represents the script word domain.
    /// </summary>
    public interface IBdoScriptDomain : IBdoLogged
    {
        /// <summary>
        /// The scope of this instance.
        /// </summary>
        IBdoScope Scope { get; set; }

        /// <summary>
        /// The variable element set of this instance.
        /// </summary>
        IBdoMetaSet VariableSet { get; set; }

        /// <summary>
        /// The script word of this instance.
        /// </summary>
        IBdoScriptword Scriptword { get; set; }
    }
}
