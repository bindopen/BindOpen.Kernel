using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Assemblies;
using System.Collections.Generic;

namespace BindOpen.Kernel.Scoping
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
        IList<BdoExtensionKinds> ExtensionKinds { get; set; }


        IList<IBdoAssemblyReference> References { get; set; }
    }
}