using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Scoping.Script;

namespace BindOpen.System.Tests
{
    public static class BdoSpecFaker
    {
        public static readonly string XmlFilePath = SystemData.WorkingFolder + "Spec.xml";

        public static IBdoSpec CreateSpec()
        {
            var spec = BdoData.NewSpec<BdoSpec>()
                .WithReference(BdoData.NewRef(BdoScript._Eq(1, 0)));

            return spec;
        }
    }
}
