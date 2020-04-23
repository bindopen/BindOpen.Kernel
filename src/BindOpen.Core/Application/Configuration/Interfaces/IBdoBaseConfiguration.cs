using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Items;

namespace BindOpen.Application.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoBaseConfiguration : IDataElementSet, INamed, ISavable, IGloballyDescribed, IIdentifiedDataItem
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