using System.Collections.Generic;

namespace BindOpen.Data.Assemblies;

/// <summary>
/// Defines a collection of assembly references.
/// This collection is used to manage groups of assemblies that should be 
/// loaded or scanned together by the BindOpen depots.
/// </summary>
public interface IBdoAssemblyReferenceCollection : IList<IBdoAssemblyReference>
{
    // This interface centralizes the management of external plugins 
    // and library dependencies for a given scope.
}