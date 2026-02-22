using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace BindOpen.Scoping.Tests;

/// <summary>
/// This class represents a fake task used to test execution logic, 
/// asynchronous processing, and automated metadata mapping in BindOpen.
/// </summary>
[BdoTask(
    Name = "taskFake",
    Description = "Test task extension.",
    CreationDate = "2023-02-24"
)]
public class TaskFake : BdoTask
{
    /// <summary>
    /// The unique extension name identified by the BindOpen Scope.
    /// </summary>
    public static string __DefinitionUniqueName = "scoping.tests$taskFake";

    // ------------------------------------------
    // PROPERTIES
    // ------------------------------------------

    #region Properties

    /// <summary>
    /// A configuration property. Static throughout the task lifecycle.
    /// </summary>
    [BdoProperty(Name = "boolValue")]
    public bool BoolValue { get; set; }

    /// <summary>
    /// A configuration property. Static throughout the task lifecycle.
    /// </summary>
    [BdoProperty(Name = "intValue")]
    public int IntValue { get; set; }

    /// <summary>
    /// An input property. Usually provided at runtime before execution.
    /// </summary>
    [BdoInput(Name = "enumValue")]
    public AccessibilityLevels EnumValue { get; set; }

    /// <summary>
    /// An output property. Populated during or after execution to return data.
    /// </summary>
    [BdoOutput(Name = "stringValue")]
    public string StringValue { get; set; }

    /// <summary>
    /// A complex output node containing a set of metadata results.
    /// </summary>
    [BdoOutput(Name = "output")]
    public BdoMetaSet Outputs { get; set; }

    /// <summary>
    /// A sub-task property demonstrating hierarchical task composition.
    /// </summary>
    [BdoProperty(Name = "subTask")]
    public TaskFake SubTask { get; set; }

    /// <summary>
    /// A specialized output flag.
    /// </summary>
    [BdoOutput("flag")]
    public string Flag { get; set; }

    /// <summary>
    /// Fluent mutator to set the task flag.
    /// </summary>
    public IBdoTask WithFlag(string flag)
    {
        Flag = flag;
        return this;
    }

    #endregion

    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    public TaskFake() : base()
    {
    }

    #endregion

    // ------------------------------------------
    // EXECUTION
    // ------------------------------------------

    /// <summary>
    /// Asynchronously executes the task logic.
    /// </summary>
    /// <param name="token">Cancellation token.</param>
    /// <param name="scope">The execution scope.</param>
    /// <param name="varSet">The variable set for script resolution.</param>
    /// <param name="runtimeMode">The execution mode (Normal, Simulation, etc.).</param>
    /// <param name="log">The execution log.</param>
    /// <returns>Returns True if the task completed successfully.</returns>
    public override Task<bool> ExecuteAsync(
        CancellationToken token,
        IBdoScope scope = null,
        IBdoMetaSet varSet = null,
        RuntimeModes runtimeMode = RuntimeModes.Normal,
        IBdoLog log = null)
    {
        // 1. Log the start of the task
        log?.AddEvent(BdoEventKinds.Checkpoint, "Task execution started.");

        // 2. Perform the logic (simulated here)
        Debug.WriteLine("TaskFake: Processing logic...");

        // 3. Populate an output (simulating a result)
        StringValue = "Execution Result: Success";

        // 4. Return completion
        return Task.FromResult(true);
    }
}