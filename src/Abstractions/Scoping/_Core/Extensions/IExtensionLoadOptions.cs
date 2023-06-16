﻿using BindOpen.System.Data;
using BindOpen.System.Data.Assemblies;
using System.Collections.Generic;

namespace BindOpen.System.Scoping
{
    /// <summary>
    /// This interface defines the extension loading options.
    /// </summary>
    public interface IExtensionLoadOptions : IBdoObject
    {
        /// <summary>
        /// 
        /// </summary>
        IList<(DatasourceKind Kind, string Uri)> Sources { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IList<BdoExtensionKind> ExtensionKinds { get; set; }


        IList<IBdoAssemblyReference> References { get; set; }
    }
}