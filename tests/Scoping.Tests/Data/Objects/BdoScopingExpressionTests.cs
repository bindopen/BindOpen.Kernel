using BindOpen.Scoping.Script;
using BindOpen.Tests;
using NUnit.Framework;

namespace BindOpen.Data
{
    /// <summary>
    /// This class provides unit tests for script-based expressions within the scoping engine.
    /// </summary>
    [TestFixture, Order(210)]
    public class BdoScopingExpressionTests
    {
        private IBdoExpression _exp;
        private BdoExpressionTests _dataTests;

        /// <summary>
        /// Initializes shared data tests and prepares the testing environment.
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // We use a shared data test instance to leverage common validation logic
            _dataTests = new BdoExpressionTests();
            _dataTests.OneTimeSetUp();
        }

        /// <summary>
        /// Tests the creation of a script expression from a function name retrieved via a data faker.
        /// </summary>
        [Test, Order(3)]
        public void Create3Test()
        {
            // 1. Retrieve test data (function names, etc.) from the faker
            var valueSet = BdoExpressionFaker.GetValueSet();
            string functionName = valueSet.ScriptwordName as string;

            // 2. Create the script expression. 
            // We ensure it is identified as a 'Script' to trigger the proper interpreter logic.
            _exp = BdoScript.Func(valueSet.ScriptwordName as string);

            // 3. Perform shared validation tests
            _dataTests.Test(_exp);

            // 4. Specific scoping assertions
            Assert.That(_exp, Is.Not.Null, "The scoping expression failed to instantiate.");
        }
    }
}