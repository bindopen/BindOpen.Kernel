using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dto;

namespace BindOpen.Framework.Core.Application.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoBaseConfiguration : IDataElementSet, INamed, IIdentifiedDataItem, ISavable, IGloballyDescribed
    {
        /// <summary>
        /// 
        /// </summary>
        string CreationDate { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        string CurrentFilePath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string LastModificationDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        IBdoBaseConfiguration AddGroup(string groupId, params IDataElement[] items);
    }
}