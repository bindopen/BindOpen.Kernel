using BindOpen.Data;
using BindOpen.Data.Meta;

namespace BindOpen.Tests;

public static class BdoSpecFaker
{
    public static readonly string XmlFilePath = DataTestData.WorkingFolder + "Spec.xml";

    public static IBdoSpec CreateSpecWithReference()
    {
        var spec = BdoData.NewSpec<BdoSpec>()
            .WithReference(BdoData.NewExp("$eq(1, 0)"));

        return spec;
    }
}
