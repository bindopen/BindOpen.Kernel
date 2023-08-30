using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Tests;
using NUnit.Framework;

namespace BindOpen.System.Scoping.Script
{
    /// <summary>
    /// 
    /// </summary>
    public class BdoScriptTests_Fluid
    {
        [Test, Order(201)]
        public void InterpreteStringThisTest()
        {
            var exp = BdoData.NewExp("$(this).(parent).value()");

            var interpreter = SystemData.Scope.Interpreter;

            var meta = BdoData.NewMeta(123).WithParent(BdoData.NewMeta(125));

            var varSet = BdoData.NewMetaSet((BdoScript.__This_VarName, meta));

            var result = interpreter.Evaluate<int?>(exp, varSet);

            Assert.That(result == 125, "Bad script interpretation");
        }

        [Test, Order(201)]
        public void InterpreteWordThisTest()
        {
            var exp = BdoData.NewExp(BdoScript._This<IBdoMetaData>().Parent().Value());

            var interpreter = SystemData.Scope.Interpreter;

            var meta = BdoData.NewMeta(123).WithParent(BdoData.NewMeta(125));

            var varSet = BdoData.NewMetaSet((BdoScript.__This_VarName, meta));

            var result = interpreter.Evaluate<int?>(exp, varSet);

            Assert.That(result == 125, "Bad script interpretation");
        }
    }
}
