using BindOpen.Data;
using BindOpen.Scoping.Script;
using BindOpen.Scoping.Tests;
using NUnit.Framework;

namespace BindOpen.Scoping.Functions;

/// <summary>
/// Contains unit tests for script functions within the BindOpen scoping engine,
/// verifying their resolution and execution from both metadata configurations and fluent scriptwords.
/// </summary>
/// <remarks>
/// These tests verify that <see cref="IBdoScope.CallFunction{T}"/> accurately resolves registered
/// function extensions (such as <c>scoping.tests$testEqual</c>), maps input arguments, and returns
/// the expected evaluated result.
/// </remarks>
[TestFixture]
public class BdoFunctionTests
{
    private readonly string _filePath = DataTestData.WorkingFolder + "Function.xml";
    private dynamic _testData;

    /// <summary>
    /// Initializes randomized function parameter data using <see cref="BdoFunctionFaker"/>
    /// before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _testData = BdoFunctionFaker.Fake();
    }

    /// <summary>
    /// Verifies that a scriptword constructed from a raw <see cref="IBdoMetaObject"/> targeting
    /// the <c>scoping.tests$testEqual</c> function extension executes properly via <see cref="IBdoScope.CallFunction{T}"/>.
    /// </summary>
    [Test]
    public void NewWordFromConfigTest()
    {
        // 1. Manually build the metadata for the function call
        // Specify the unique DataType that points to the 'testEqual' extension
        var meta = BdoMetaSetExtensions.Add(
            BdoData.NewObject()
                .WithDataType(BdoExtensionKinds.Function, "scoping.tests$testEqual"),
            BdoData.NewScalar("stringValue", _testData.stringValue as string),
            BdoData.NewScalar("intValue", _testData.intValue as int?));

        // 2. Convert the metadata into a script word
        var word = BdoScript.NewWord(meta);

        // 3. Call the function through the scoping engine
        var result = ScopingTestData.Scope?.CallFunction<bool?>(word);

        // 4. Assert the result (expecting false based on fake data logic)
        Assert.That(result, Is.False, "Function execution via metadata configuration failed.");
    }

    /// <summary>
    /// Verifies that a script function call constructed via the fluent <see cref="BdoScript.Func"/> API
    /// executes properly through the scoping engine and returns the expected boolean result.
    /// </summary>
    [Test]
    public void NewFunTest()
    {
        // 1. Create a function word using the simplified fluent syntax
        var word = BdoScript.Func(
            "testEqual",
            _testData.stringValue as string,
            _testData.intValue as int?);

        // 2. Call the function through the scoping engine
        var result = ScopingTestData.Scope?.CallFunction<bool?>(word);

        // 3. Assert the result
        Assert.That(result, Is.False, "Function execution via fluent scriptword failed.");
    }
}