﻿using BindOpen.System.Hosting.Hosts;

namespace BindOpen.System.Hosting
{
    /// <summary>
    /// The interface defines a hosted item.
    /// </summary>
    public interface IBdoHosted
    {
        // Execution ---------------------------------

        /// <summary>
        /// The host of the instance.
        /// </summary>
        IBdoHost Host { get; }
    }
}