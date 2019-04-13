using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Items.Carriers;
using BindOpen.Framework.Core.Extensions.Items.Entities;
using BindOpen.Framework.Core.Extensions.Items.Formats;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Items.Documents
{
    public interface IDocument
    {
        CarrierDto Container { get; set; }
        EntityDto Content { get; set; }

        IFormatDto DetectFormat(IDataSource dataSource, ref ILog log);
        ILog Update(IDocument documentDataItem, string relativePath = "");
    }
}