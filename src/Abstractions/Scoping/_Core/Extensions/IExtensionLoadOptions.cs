using BindOpen.Data;
using BindOpen.Data.Assemblies;
using System.Collections.Generic;

namespace BindOpen.Scoping
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