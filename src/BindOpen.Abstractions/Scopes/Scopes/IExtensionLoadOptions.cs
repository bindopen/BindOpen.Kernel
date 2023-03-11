using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Extensions;
using System.Collections.Generic;

namespace BindOpen.Scopes.Scopes
{
    /// <summary>
    /// This interface defines the extension loading options.
    /// </summary>
    public interface IExtensionLoadOptions : IBdoItem
    {
        /// <summary>
        /// 
        /// </summary>
        List<(DatasourceKind Kind, string Uri)> Sources { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<BdoExtensionKind> ExtensionKinds { get; set; }


        List<IBdoAssemblyReference> References { get; set; }
    }
}