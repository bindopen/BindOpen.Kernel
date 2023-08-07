using BindOpen.System.Data;
using BindOpen.System.Data.Meta;

namespace BindOpen.System.Tests
{
    public static class BdoSpecFaker
    {
        public static readonly string XmlFilePath = SystemData.WorkingFolder + "Spec.xml";

        public static IBdoSpec CreateSpec()
        {
            var spec = BdoData.NewSpec();

            return spec;
        }
    }
}
