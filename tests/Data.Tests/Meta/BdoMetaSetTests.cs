using Bogus;
using NUnit.Framework;
using System.Threading.Tasks;

namespace BindOpen.Data.Meta;

/// <summary>
/// Contains unit tests for <see cref="IBdoMetaSet"/> operations, focusing on conditional execution.
/// </summary>
/// <remarks>
/// These tests verify the framework's ability to invoke synchronous and asynchronous actions 
/// based on the presence of specific metadata within a set.
/// </remarks>
[TestFixture, Order(210)]
public class BdoMetaSetTests
{
    /// <summary>
    /// Performs one-time setup for the metadata set tests.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Faker is initialized here if randomized data is needed for future tests.
        var f = new Faker();
    }

    /// <summary>
    /// Tests the conditional invocation logic of a metadata set.
    /// </summary>
    /// <remarks>
    /// This test ensures that <see cref="IBdoMetaSet.Invoke"/> and <see cref="IBdoMetaSet.InvokeAsync"/> 
    /// correctly evaluate the predicate (existence of "path") before executing the provided action.
    /// </remarks>
    [Test, Order(1)]
    public async Task Create1Test()
    {
        // 1. Setup a set with a simple scalar
        var set = BdoData.NewSet(BdoData.NewScalar("path", "<mypath>"));

        // 2. Testing Synchronous Invocation
        var isInvokedSync = false;
        set.Invoke(q => q.Has("path"), () => { isInvokedSync = true; });

        Assert.That(isInvokedSync, Is.True, "The synchronous Invoke action should have been triggered since the 'path' key exists.");

        // 3. Testing Asynchronous Invocation
        var isInvokedAsync = false;
        await set.InvokeAsync(q => q.Has("path"), () =>
        {
            isInvokedAsync = true;
            return Task.CompletedTask;
        });

        Assert.That(isInvokedAsync, Is.True, "The asynchronous InvokeAsync action should have been triggered since the 'path' key exists.");
    }
}