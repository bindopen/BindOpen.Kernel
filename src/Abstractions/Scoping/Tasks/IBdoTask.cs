using BindOpen.Hosting;

namespace BindOpen.Scoping.Tasks;

/// <summary>
/// Defines a BindOpen task, representing a manageable and executable unit of work.
/// </summary>
/// <remarks>
/// A task acts as both an extension (identifiable and configurable) and a process 
/// (executable with a lifecycle), allowing it to be orchestrated within a scope.
/// </remarks>
public interface IBdoTask : IBdoExtension, IBdoProcess
{
}