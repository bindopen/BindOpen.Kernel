using BindOpen.Data.Meta;
using NUnit.Framework;

namespace BindOpen.Scoping.Script
{
    /// <summary>
    /// This class provides unit tests for the string representation of BdoScriptwords,
    /// ensuring that fluently built script trees are correctly serialized into script strings.
    /// </summary>
    [TestFixture, Order(401)]
    public class BdoScriptwordTests_ToString
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Initial setup logic if required
        }

        /// <summary>
        /// Tests the conversion of a fluidly constructed scriptword tree into its 
        /// corresponding script string representation.
        /// </summary>
        [Test, Order(1)]
        public void ToStringTest()
        {
            // 1. Build a fluid scriptword: $(this).(parent).value()
            var word = BdoScript.This<IBdoMetaData>()._Parent()._Value();

            // 2. Verify the full chain serialization
            var script = word?.ToString();
            Assert.That(script, Is.EqualTo("$('$this').('parent').value()"), "The full scriptword serialization is incorrect.");

            // 3. Verify the parent part serialization
            // Note: In BindOpen, Parent property navigates up the AST tree
            script = word.Parent?.ToString();
            Assert.That(script, Is.EqualTo("$('$this').('parent')"), "The parent scriptword serialization is incorrect.");

            // 4. Verify the root part serialization
            script = word.Parent?.Parent?.ToString();
            Assert.That(script, Is.EqualTo("$('$this').('parent').value()"), "The root scriptword serialization is incorrect.");
        }
    }
}