using BindOpen.Data.Meta;
using NUnit.Framework;

namespace BindOpen.Scoping.Script
{
    [TestFixture, Order(401)]
    public class BdoScriptTests_ToString
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(1)]
        public void ToStringTest()
        {
            var word = BdoScript.This<IBdoMetaData>()._Parent().Value();

            var script = word?.ToString();
            Assert.That(script == "$('$this').('parent').value()", "Bad script interpretation");

            script = word.Parent?.ToString();
            Assert.That(script == "$('$this').('parent')", "Bad script interpretation");

            script = word.Parent?.Parent?.ToString();
            Assert.That(script == "$('$this').('parent').value()", "Bad script interpretation");
        }
    }
}
