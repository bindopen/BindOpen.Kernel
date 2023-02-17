﻿using BindOpen.Data;
using BindOpen.Data.Items;
using BindOpen.Dtos.Json;
using BindOpen.Dtos.Xml;
using BindOpen.Extensions.Scripting;
using BindOpen.Tests.Runtime;
using Bogus;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests.IO.Data
{
    [TestFixture, Order(210)]
    public class ExpressionIOTests
    {
        private readonly string _filePath_xml = Tests.WorkingFolder + "DataExpression.xml";
        private readonly string _filePath_json = Tests.WorkingFolder + "DataExpression.json";
        dynamic _valueSet;
        private IBdoExpression _exp = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var f = new Faker();
            _valueSet = new
            {
                Script = "$(var1)",
                Literal = f.Random.Word(),
                ScriptwordName = "func1",
                ExpressionKind = f.PickRandom<BdoExpressionKind>()
            };
        }

        public static bool Equals(
            IBdoExpression exp1,
            IBdoExpression exp2)
        {
            var b = exp1 != null && exp2 != null
                && exp1.IsDeepEqual(exp2);
            return b;
        }

        [Test, Order(1)]
        public void CreateTest()
        {
            _exp = BdoData.NewExpression(
                _valueSet.Literal as string,
                _valueSet.ExpressionKind as BdoExpressionKind? ?? BdoExpressionKind.Auto)
                .WithWord(BdoScript.Func("Fun1").Func("Fun2", BdoScript.Var("var1")));
        }

        // Xml

        [Test, Order(3)]
        public void SaveXmlTest()
        {
            if (_exp == null)
            {
                CreateTest();
            }

            var isSaved = _exp.ToDto().SaveXml(_filePath_xml);
            Assert.That(isSaved, "Expression saving failed");
        }

        [Test, Order(4)]
        public void LoadXmlTest()
        {
            if (_exp == null || !File.Exists(_filePath_xml))
            {
                SaveXmlTest();
            }

            var exp = RuntimeTests.Scope.ConvertToPoco(XmlHelper.LoadXml<ExpressionDto>(_filePath_xml));
            Assert.That(Equals(exp, _exp), "Error while loading");
        }

        // Json

        [Test, Order(5)]
        public void SaveJsonTest()
        {
            if (_exp == null)
            {
                CreateTest();
            }

            var isSaved = _exp.ToDto().SaveJson(_filePath_json);
            Assert.That(isSaved, "Expression saving failed");
        }

        [Test, Order(6)]
        public void LoadJsonTest()
        {
            if (_exp == null || !File.Exists(_filePath_json))
            {
                SaveJsonTest();
            }

            var exp = RuntimeTests.Scope.ConvertToPoco(JsonHelper.LoadJson<ExpressionDto>(_filePath_json));
            Assert.That(Equals(exp, _exp), "Error while loading");
        }
    }
}
