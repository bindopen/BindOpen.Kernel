using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Scoping.Script;

namespace BindOpen.Kernel.Tests
{
    public static class BdoSpecFaker
    {
        public static readonly string XmlFilePath = SystemData.WorkingFolder + "Spec.xml";

        public static IBdoSpec CreateSpec()
        {
            var spec = BdoData.NewSpec<BdoSpec>()
                .WithReference(BdoData.NewRef(BdoScript.Eq(1, 0)));

            return spec;
        }
    }
}
