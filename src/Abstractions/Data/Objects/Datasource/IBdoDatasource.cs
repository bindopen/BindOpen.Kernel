using BindOpen.System.Data.Meta;

namespace BindOpen.System.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDatasource :
        ITBdoSet<IBdoMetaObject>, IDefaultable,
        IBdoObjectNotMetable, IIdentified, INamed, IReferenced
    {
        /// <summary>
        /// 
        /// </summary>
        string InstanceName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DatasourceKind Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ModuleName { get; set; }
    }
}