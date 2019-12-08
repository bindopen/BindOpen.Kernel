using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Runtime.Application.Options;
using BindOpen.Framework.Runtime.System;

namespace BindOpen.Framework.Runtime.Application.Modules
{
    /// <summary>
    /// 
    /// </summary>
    public interface IApplicationModuleInstance : IDescribedDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        string AbsoluteUri { get; }

        /// <summary>
        /// 
        /// </summary>
        AccessibilityLevels AccessibilityLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ApplicationExecutionPath { get; }

        /// <summary>
        /// 
        /// </summary>
        InstanceIndexations Indexation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsLocal { get; }

        /// <summary>
        /// 
        /// </summary>
        ApplicationModuleKind Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IAppModule Module { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ModuleName { get; }

        /// <summary>
        /// 
        /// </summary>
        IOptionSet OptionSet { get; }

        /// <summary>
        /// 
        /// </summary>
        IDataItemSet<AppSection> Sections { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ApplicationModuleSubKind SubKind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Uri { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="completeName"></param>
        /// <returns></returns>
        IAppSection GetSectionWithCompleteName(string completeName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IAppSection GetSectionWithName(string name);
    }
}