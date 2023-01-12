﻿using BindOpen.Extensions;
using BindOpen.Meta;
using BindOpen.Meta.Items;
using NUnit.Framework;

namespace BindOpen.Runtime.Tests.MetaData.Items
{
    [TestFixture, Order(210)]
    public class BdoSourceTests
    {
        private IBdoDataSource _datasource;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(1)]
        public void CreateDatasourceTest()
        {
            _datasource = BdoMeta.NewDatasource("name", DatasourceKind.Database)
                .WithConfiguration(
                    BdoExtensions.NewConnectorConfiguration("tests.core$test")
                        .WithConnectionString("connectionString"));

            Assert.That(
                _datasource != null, "Bad data source creation");
        }

        public static void Test(IBdoDataSource source)
        {
            Assert.That(
                source.GetConfiguration("tests.core$test") != null, "Datasource - Configuration not found");

            Assert.That(
                source.GetConfiguration("tests.core$test").GetConnectionString() == "connectionString", "Datasource - Configuration not found");
        }
    }
}
