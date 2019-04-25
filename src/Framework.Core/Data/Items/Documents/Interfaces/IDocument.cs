using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Items.Carriers;
using BindOpen.Framework.Core.Extensions.Items.Entities;
using BindOpen.Framework.Core.Extensions.Items.Formats;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Items.Documents
{
    public interface IDocument
    {
        CarrierConfiguration Container { get; set; }
        EntityConfiguration Content { get; set; }

        IFormatConfiguration DetectFormat(IDataSource dataSource, ref ILog log);
        ILog Update(IDocument documentDataItem, string relativePath = "");
    }
}