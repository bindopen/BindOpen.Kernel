using BindOpen.Data.Meta;
using NUnit.Framework;

namespace BindOpen.Scoping.Script;

/// <summary>
/// Contains unit tests for the string serialization of <see cref="IBdoScriptword"/> AST trees,
/// ensuring that fluently built script chains serialize accurately into canonical BindOpen script strings.
/// </summary>
/// <remarks>
/// These tests verify both full chained scriptword serialization and intermediate AST node serialization
/// by traversing upward via the <see cref="IBdoScriptword.Parent"/> navigation property.
/// </remarks>
[TestFixture]
public class BdoScriptwordTests_ToString
{
    /// <summary>
    /// Performs one-time setup before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Initial setup logic if required
    }

    /// <summary>
    /// Verifies that a fluently constructed scriptword chain (<c>$(this).(parent).value()</c>)
    /// and each of its ancestor nodes serialize to their expected BindOpen script representations.
    /// </summary>
    [Test]
    public void ToStringTest()
    {
        // 1. Build a fluid scriptword: $(this).(parent).value()
        var word = BdoScript.This<IBdoMetaData>()._Parent()._Value();

        Assert.Multiple(() =>
        {
            // 2. Verify the full chain serialization
            var script = word?.ToString();
            Assert.That(script, Is.EqualTo("$('$this').('parent').value()"), "The full scriptword serialization is incorrect.");

            // 3. Verify the parent part serialization
            // Note: In BindOpen, Parent property navigates up the AST tree
            script = word.Parent?.ToString();
            Assert.That(script, Is.EqualTo("$('$this').('parent')"), "The parent scriptword serialization is incorrect.");

            // 4. Verify the root part serialization
            script = word.Parent?.Parent?.ToString();
            Assert.That(script, Is.EqualTo("$('$this')"), "The root scriptword serialization is incorrect.");
        });
    }
}