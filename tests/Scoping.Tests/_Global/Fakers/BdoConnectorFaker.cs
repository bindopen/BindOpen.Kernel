using BindOpen.Data;
using BindOpen.Data.Meta;
using Bogus;
using System.Dynamic;

namespace BindOpen.Scoping.Tests;

public static class BdoConnectorFaker
{
    public static dynamic NewData()
    {
        var f = new Faker();
        dynamic b = new ExpandoObject();
        b.connectionString = f.Random.Word();
        b.host = f.Internet.IpAddress().ToString();
        b.port = f.Random.Int(800);
        b.isSslEnabled = f.Random.Bool();
        return b;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param key="data"></param>
    /// <returns></returns>
    public static IBdoMetaObject NewMetaObject(dynamic data = null)
    {
        data ??= NewData();

        var config =
            BdoData.NewObject()
            .WithDataType(BdoExtensionKinds.Connector, "scoping.tests$testConnector")
            .With(
                BdoData.NewScalar("host", data.host as string),
                BdoData.NewScalar("port", data.port as int?),
                BdoData.NewScalar("isSslEnabled", data.isSslEnabled as bool?));

        return config;
    }
}
