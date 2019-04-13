using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Runtime.Application.Languages;

namespace BindOpen.Framework.Runtime.Application.Modules
{
    public interface IAppModule : IDescribedDataItem
    {
        string DefaultUICulture { get; set; }
        string IconFileName { get; set; }
        bool IsVisible { get; set; }
        IDataItemSet<ApplicationLanguage> Languages { get; set; }
        int Rank { get; set; }
        IDataItemSet<AppSection> Sections { get; set; }
        string ThumbIconFileName { get; set; }

        IAppModule AddSection(IAppSection section);
        IAppModule AddSections(params IAppSection[] sections);
        IApplicationLanguage GetLanguageWithUICulture(string uiCulureName);
        IAppSection GetSubSectionWithName(string name);
        IAppSection GetSubSectionWithUniqueName(string completeName);
    }
}