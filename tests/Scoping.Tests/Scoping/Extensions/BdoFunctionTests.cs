using BindOpen.Data;
using BindOpen.Scoping.Script;
using BindOpen.Scoping.Tests;
using NUnit.Framework;

namespace BindOpen.Scoping.Functions;

/// <summary>
/// This class provides unit tests for script functions, verifying their resolution 
/// and execution within the BindOpen scoping engine.
/// </summary>
[TestFixture, Order(300)]
public class BdoFunctionTests
{
    private readonly string _filePath = DataTestData.WorkingFolder + "Function.xml";
    private dynamic _testData;

    /// <summary>
    /// Initializes the test data using the function faker.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _testData = BdoFunctionFaker.Fake();
    }

    /// <summary>
    /// Tests the creation of a script word from a raw metadata configuration 
    /// and its execution via the Scope.
    /// </summary>
    [Test, Order(1)]
    public void NewWordFromConfigTest()
    {
        // 1. Manually build the metadata for the function call
        // We specify the unique DataType that points to the 'testEqual' extension
        var meta = BdoData.NewObject()
            .WithDataType(BdoExtensionKinds.Function, "scoping.tests$testEqual")
            .With(
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
    /// Tests the creation of a script function call using the fluent BdoScript API.
    /// </summary>
    [Test, Order(2)]
    public void NewFunTest()
    {
        // 1. Create a function word using the simplified fluent syntax
        var word = BdoScript.Func("testEqual",
                _testData.stringValue as string,
                _testData.intValue as int?);

        // 2. Call the function through the scoping engine
        var result = ScopingTestData.Scope?.CallFunction<bool?>(word);

        // 3. Assert the result
        Assert.That(result, Is.False, "Function execution via fluent scriptword failed.");
    }
}