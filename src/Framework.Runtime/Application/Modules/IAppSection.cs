using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dto;

namespace BindOpen.Framework.Runtime.Application.Modules
{
    public interface IAppSection : IDescribedDataItem
    {
        string IconFileName { get; set; }
        bool IsVisible { get; set; }
        IAppModule Module { get; set; }
        IAppSection Parent { get; set; }
        int Rank { get; set; }
        Core.Data.Items.Sets.DataItemSet<AppSection> SubSections { get; set; }
        string ThumbIconFileName { get; set; }

        void AddSection(IAppSection section);
        IAppSection AddSections(params AppSection[] sections);
        IAppSection GetSubSectionWithName(string name);
        IAppSection GetSubSectionWithUniqueName(string key);
        string Key();
    }
}