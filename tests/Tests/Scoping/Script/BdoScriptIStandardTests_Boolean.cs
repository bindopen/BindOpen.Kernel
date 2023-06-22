using BindOpen.System.Tests;
using NUnit.Framework;

namespace BindOpen.System.Scoping.Script
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BdoScriptIStandardTests
    {
        [Test, Order(201)]
        public void EqTest()
        {
            var word = BdoScript._Eq("MYTABLE", BdoScript._Text("MYTABLE"));

            var interpreter = SystemData.Scope.Interpreter;
            var result = interpreter.Evaluate<bool?>(word);

            Assert.That(result == true, "Bad script interpretation");
        }
    }
}
