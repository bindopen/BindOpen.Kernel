using BindOpen.Framework.Core.Extensions.Definition.Carriers;

namespace BindOpen.Framework.Core.Extensions.Items.Carriers
{
    public interface ICarrierDto : ITAppExtensionTitledItemDto<ICarrierDefinition>
    {
        string Flag { get; set; }
        bool IsReadonly { get; set; }
        string LastAccessDate { get; set; }
        string LastWriteDate { get; set; }
        string ParentPath { get; set; }
        bool ParentPathSpecified { get; }
        string Path { get; set; }
    }
}