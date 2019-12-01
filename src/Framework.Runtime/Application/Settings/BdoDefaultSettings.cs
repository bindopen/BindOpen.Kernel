using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Schema;

namespace BindOpen.Framework.Runtime.Application.Settings
{
    /// <summary>
    /// This class represents a base settings.
    /// </summary>
    public class BdoDefaultSettings : TBdoSettings<BdoBaseConfiguration>
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoDefaultSettings class.
        /// </summary>
        public BdoDefaultSettings()
        {
            _configuration = new BdoBaseConfiguration();
        }

        /// <summary>
        /// Instantiates a new instance of the BdoDefaultSettings class.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        public BdoDefaultSettings(IBdoScope scope, BdoBaseConfiguration configuration) : base(scope, configuration)
        {
        }

        #endregion
    }
}
