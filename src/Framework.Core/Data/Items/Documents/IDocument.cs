using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Configuration.Carriers;
using BindOpen.Framework.Core.Extensions.Configuration.Entities;
using BindOpen.Framework.Core.Extensions.Configuration.Formats;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Items.Documents
{
    public interface IDocument
    {
        ICarrierConfiguration Container { get; set; }
        IEntityConfiguration Content { get; set; }

        object Clone();
        IFormatConfiguration DetectFormat(IDataSource dataSource, ref ILog log);
        ILog Update(IDocument documentDataItem, string relativePath = "");
    }
}