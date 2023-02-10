using BindOpen.Data;
using BindOpen.Data.Items;
using BindOpen.Extensions.Scripting;
using Bogus;
using NUnit.Framework;

namespace BindOpen.Tests.Data
{
    [TestFixture, Order(210)]
    public class BdoReferenceTests
    {
        dynamic _valueSet;
        private IBdoReference _exp = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var f = new Faker();
            _valueSet = new
            {
                Script = "$(var1)",
                Literal = f.Random.Word(),
                ScriptwordName = "func1"
            };
        }

        public void Test(IBdoReference exp)
        {
        }

        [Test, Order(1)]
        public void Create1Test()
        {
            _exp = BdoData.NewReference(
                BdoMeta.New(source),
                "standard$GetTable",
                BdoMeta.NewList(
                    ("schema", "Crm"),
                    ("table", "Contact"))
                )
                .With
                _valueSet.Literal as string,
                BdoReferenceKind.Literal);

            Test(_exp);
        }

        [Test, Order(2)]
        public void Create2Test()
        {
            _exp = BdoData.NewReference(_valueSet.Script as string)
                .AsScript();

            Test(_exp);
        }

        [Test, Order(3)]
        public void Create3Test()
        {
            _exp = BdoData.NewExp(
                BdoScript.Func(_valueSet.ScriptwordName as string));

            Test(_exp);
        }
    }
}
