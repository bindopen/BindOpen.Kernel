using BindOpen.Data;
using BindOpen.Data.Meta;
using Bogus;
using System.Dynamic;

namespace BindOpen.Scoping.Tests;

/// <summary>
/// This class provides factory methods to generate randomized data and meta-objects
/// for testing BdoConnectors.
/// </summary>
public static class BdoConnectorFaker
{
    /// <summary>
    /// Generates a randomized set of connector data using the Bogus library.
    /// </summary>
    /// <returns>A dynamic object containing connection properties.</returns>
    public static dynamic NewData()
    {
        var f = new Faker();
        dynamic b = new ExpandoObject();

        b.connectionString = f.Random.Word();
        b.host = f.Internet.IpAddress().ToString();
        b.port = f.Random.Int(800, 9000); // Standardized port range
        b.isSslEnabled = f.Random.Bool();

        return b;
    }

    /// <summary>
    /// Creates a new BdoMetaObject initialized with connector data.
    /// </summary>
    /// <param name="data">The dynamic data to use. If null, new data is generated.</param>
    /// <returns>An IBdoMetaObject configured for a test connector.</returns>
    public static IBdoMetaObject NewMetaObject(dynamic data = null)
    {
        data ??= NewData();

        // Create the meta-object and assign the connector extension ID
        var config = BdoMetaSetExtensions.Add(
BdoData.NewObject()
            .WithDataType(BdoExtensionKinds.Connector, "scoping.tests$testConnector")
, BdoData.NewScalar("host", data.host as string),
                BdoData.NewScalar("port", data.port as int?),
                BdoData.NewScalar("isSslEnabled", data.isSslEnabled as bool?));

        return config;
    }
}