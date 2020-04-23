using BindOpen.Data.Items;
using BindOpen.Application.Languages;

namespace BindOpen.Application.Modules
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppModule : IDescribedDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        string DefaultUICulture { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string IconFileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsVisible { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IDataItemSet<ApplicationLanguage> Languages { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int Rank { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IDataItemSet<AppSection> Sections { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ThumbIconFileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        IAppModule AddSection(IAppSection section);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sections"></param>
        /// <returns></returns>
        IAppModule AddSections(params IAppSection[] sections);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiCulureName"></param>
        /// <returns></returns>
        IApplicationLanguage GetLanguageWithUICulture(string uiCulureName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IAppSection GetSubSectionWithName(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="completeName"></param>
        /// <returns></returns>
        IAppSection GetSubSectionWithUniqueName(string completeName);
    }
}