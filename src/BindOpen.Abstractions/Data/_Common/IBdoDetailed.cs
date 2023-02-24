﻿using BindOpen.Data.Meta;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents a data with detail.
    /// </summary>
    public interface IBdoDetailed
    {
        /// <summary>
        /// The detail of this instance.
        /// </summary>
        IBdoMetaSet Detail { get; set; }
    }
}