using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta;

namespace BindOpen.Data.Stores;

/// <summary>
/// Defines a BindOpen data source.
/// A datasource encapsulates connection information and the type of external 
/// system (Database, File, API) being accessed.
/// </summary>
public interface IBdoDatasource : ITBdoMetaWrapper<IBdoMetaSet>, INamed, IReferenced, IBdoDataTyped
{
    /// <summary>
    /// The metadata key used to store or retrieve the datasource kind.
    /// </summary>
    public static string __ConnectionString_DatasourceKind = "datasourceKind";

    /// <summary>
    /// The metadata key used to store or retrieve the raw connection string.
    /// </summary>
    public static string __ConnectionString_Token = "connectionString";

    /// <summary>
    /// Gets or sets the kind of datasource (e.g., Database, Repository, Email Server).
    /// </summary>
    DatasourceKind DatasourceKind { get; set; }

    /// <summary>
    /// Gets or sets the connection string used to access the data source.
    /// This value can be a literal string or a resolved metadata expression.
    /// </summary>
    string ConnectionString { get; set; }
}