using System;
using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions;

namespace BindOpen.Framework.Core.Extensions.Attributes
{
    /// <summary>
    /// This class represents an attribute of handlers.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class HandlerAttribute : AppExtensionItemAttribute
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The filters of this instance.
        /// </summary>
        public List<IAppExtensionFilter> Filters { get; set; } = new List<IAppExtensionFilter>();

        /// <summary>
        /// The source kinds of this instance.
        /// </summary>
        public List<DataSourceKind> DefaultSourceKinds { get; set; } = null;

        /// <summary>
        /// The path of the folder of this instance.
        /// </summary>
        public string DefaultFolderPath { get; set; } = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the HandlerAttribute class.
        /// </summary>
        public HandlerAttribute() : base()
        {
        }

        #endregion
    }
}
