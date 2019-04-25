using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Extensions.Definitions.Carriers;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Items.Carriers
{
    public interface ICarrier : ITAppExtensionItem<ICarrierDefinition>
    {
        ICarrierElement AsElement(string name = null, Log log = null);

        string RelativePath { get; set; }

        void SetPath(string path = null, string relativePath = null);

        string CreationDate { get; set; }

        string Flag { get; set; }

        bool IsReadonly { get; set; }

        string LastAccessDate { get; set; }

        string LastWriteDate { get; set; }
    }
}