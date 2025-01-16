using BindOpen.Data;
using BindOpen.Data.Schema;

namespace BindOpen.Tests;

public static class BdoSchemaFaker
{
    public static readonly string XmlFilePath = DataTestData.WorkingFolder + "Spec.xml";

    public static IBdoSchema CreateSpecWithReference()
    {
        var schema = BdoData.NewSchema<BdoSchema>()
            .WithReference(BdoData.NewExp("$eq(1, 0)"));

        return schema;
    }
}
