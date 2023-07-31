﻿using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Scoping;
using NUnit.Framework;

namespace BindOpen.System.Data
{
    [TestFixture, Order(210)]
    public class BdoSourceTests
    {
        private IBdoDatasource _datasource;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(1)]
        public void CreateDatasourceTest()
        {
            _datasource = BdoData.NewDatasource("name", DatasourceKind.Database)
                .With(
                    BdoData.NewConfig("bindopen.system.tests$test")
                        .WithConnectionString("connectionString"));

            Assert.That(
                _datasource != null, "Bad data source creation");
        }

        public static void Test(IBdoDatasource source)
        {
            Assert.That(
                source.Get("bindopen.system.tests$test") != null, "Datasource - Configuration not found");

            Assert.That(
                source.Get("bindopen.system.tests$test").GetConnectionString() == "connectionString", "Datasource - Configuration not found");
        }
    }
}
