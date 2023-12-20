using BindOpen.Data;
using BindOpen.Data.Meta;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// This interface defines a data source.
    /// </summary>
    public interface IBdoDatasource : ITBdoMetaWrapper<IBdoMetaNode>, INamed, IReferenced, IBdoDataTyped
    {
        public static string __ConnectionString_DatasourceKind = "datasourceKind";
        public static string __ConnectionString_Token = "connectionString";

        /// <summary>
        /// 
        /// </summary>
        DatasourceKind DatasourceKind { get; set; }


        string ConnectionString { get; set; }
    }
}