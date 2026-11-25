using Bogus;
using NUnit.Framework;
using System.Threading.Tasks;

namespace BindOpen.Data.Meta;

/// <summary>
/// Contains unit tests for <see cref="IBdoMetaSet"/> operations, focusing on conditional execution.
/// </summary>
/// <remarks>
/// These tests verify the framework's ability to evaluate predicates and invoke both synchronous
/// and asynchronous actions based on the presence of specific metadata within a set.
/// </remarks>
[TestFixture]
public class BdoMetaSetTests
{
    /// <summary>
    /// Performs one-time setup before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Faker is initialized here if randomized data is needed for future tests.
        var f = new Faker();
    }

    /// <summary>
    /// Verifies the conditional invocation logic of an <see cref="IBdoMetaSet"/>.
    /// </summary>
    /// <remarks>
    /// Ensures that <see cref="IBdoMetaSet.Invoke"/> and <see cref="IBdoMetaSet.InvokeAsync"/>
    /// properly evaluate the predicate (checking for the existence of the <c>"path"</c> key)
    /// before executing the provided callback delegates.
    /// </remarks>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test operation.</returns>
    [Test]
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