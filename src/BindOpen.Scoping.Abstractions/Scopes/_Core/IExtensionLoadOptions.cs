using BindOpen.Scoping.Data;
using BindOpen.Scoping.Data.Assemblies;
using BindOpen.Scoping.Extensions;
using System.Collections.Generic;

namespace BindOpen.Scoping.Scopes
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