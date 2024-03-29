﻿namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines a globally-described data.
    /// </summary>
    public interface IBdoDescribed
    {
        /// <summary>
        /// The description of this object that is a string dictionary.
        /// </summary>
        ITBdoDictionary<string> Description { get; set; }
    }
}
