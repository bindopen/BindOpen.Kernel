using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Definition.Carriers;

namespace BindOpen.Framework.Core.Extensions.Items.Carriers
{
    public interface ICarrier : ITAppExtensionItem<ICarrierDefinition>
    {
        string RelativePath { get; set; }

        void SetPath(string path = null, string relativePath = null);
    }
}