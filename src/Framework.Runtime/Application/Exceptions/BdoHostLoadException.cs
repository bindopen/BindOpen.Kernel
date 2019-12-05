using System;

namespace BindOpen.Framework.Runtime.Application.Exceptions
{
    /// <summary>
    /// This static class lists the BindOpen host load exceptions.
    /// </summary>
    public class BdoHostLoadException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the BdoHostLoadException class.
        /// </summary>
        public BdoHostLoadException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BdoHostLoadException class.
        /// </summary>
        /// <param name="title">The title to consider.</param>
        public BdoHostLoadException(string title) : base(title)
        {
        }
    }
}