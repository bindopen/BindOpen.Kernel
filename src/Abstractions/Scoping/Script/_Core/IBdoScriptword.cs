﻿using BindOpen.Data;
using BindOpen.Data.Meta;

namespace BindOpen.Scoping.Script
{
    /// <summary>
    /// This interface defines a script word.
    /// </summary>
    public interface IBdoScriptword : IBdoMetaObject, ITSingleChildParent<IBdoScriptword>, IBdoExpression
    {
        /// <summary>
        /// The kind of this instance.
        /// </summary>
        ScriptTokenKinds TokenKind { get; set; }

        /// <summary>
        /// Gets the last script word of this instance considering the specified maximum level.
        /// </summary>
        /// <param name="levelMax">The maximum level to consider.</param>
        /// <returns>Returns the last script word of this instance.</returns>
        IBdoScriptword Last(int levelMax = 50);
    }
}