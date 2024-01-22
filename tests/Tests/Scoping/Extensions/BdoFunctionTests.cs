using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Scoping.Script;
using BindOpen.Kernel.Tests;
using NUnit.Framework;

namespace BindOpen.Scoping.Functions
{
    [TestFixture, Order(300)]
    public class BdoFunctionTests
    {
        private readonly string _filePath = SystemData.WorkingFolder + "Function.xml";

        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testData = BdoFunctionFaker.Fake();
        }

        [Test, Order(1)]
        public void NewWordFromConfigTest()
        {
            var meta = BdoData.NewObject()
                .WithDataType(BdoExtensionKinds.Function, "bindopen.tests$testEqual")
                .With(
                    BdoData.NewScalar("stringValue", _testData.stringValue as string),
                    BdoData.NewScalar("intValue", _testData.intValue as int?));

            var word = BdoScript.NewWord(meta);

            var result = SystemData.Scope?.CallFunction<bool?>(word);

            Assert.That(result == false, "Bad function result");
        }

        [Test, Order(2)]
        public void NewFunTest()
        {
            var word = BdoScript.Func("testEqual",
                    _testData.stringValue as string,
                    _testData.intValue as int?);

            var result = SystemData.Scope?.CallFunction<bool?>(word);

            Assert.That(result == false, "Bad function result");
        }
    }

}
