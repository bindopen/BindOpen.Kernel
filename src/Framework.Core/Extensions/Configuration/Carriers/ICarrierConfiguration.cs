using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions.Definition.Carriers;

namespace BindOpen.Framework.Core.Extensions.Configuration.Carriers
{
    public interface ICarrierConfiguration : ITAppExtensionItemConfiguration<ICarrierDefinition>
    {
        IDataElementSet Detail { get; set; }
        string Flag { get; set; }
        bool IsReadonly { get; set; }
        string LastAccessDate { get; set; }
        string LastWriteDate { get; set; }
        string ParentPath { get; set; }
        bool ParentPathSpecified { get; }
        string Path { get; set; }

        IDataElementSet NewDetail();
    }
}