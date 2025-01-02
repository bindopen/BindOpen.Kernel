using BindOpen.Data.Meta;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BindOpen.Data.Assemblies;

/// <summary>
/// This class represents a class reference database entity.
/// </summary>
public class ClassReferenceDb : IBdoDb
{
    // --------------------------------------------------
    // PROPERTIES
    // --------------------------------------------------

    #region Properties

    /// <summary>
    /// The class name of this instance.
    /// </summary>
    [Key]
    [Column("ClassReferenceId")]
    public string Identifier { get; set; }

    /// <summary>
    /// The assembly name of this instance.
    /// </summary>
    public string AssemblyName { get; set; }

    /// <summary>
    /// The assembly version of this instance.
    /// </summary>
    public string AssemblyVersion { get; set; }

    /// <summary>
    /// The assembly file name of this instance.
    /// </summary>
    public string AssemblyFileName { get; set; }

    /// <summary>
    /// The class name of this instance.
    /// </summary>
    public string ClassName { get; set; }

    /// <summary>
    /// The class reference of this instance.
    /// </summary>
    [ForeignKey(nameof(MetaDataId))]
    public MetaDataDb MetaData { get; set; }

    /// <summary>
    /// The class name of this instance.
    /// </summary>
    public string MetaDataId { get; set; }

    #endregion

    // --------------------------------------------------
    // CONSTRUCTORS
    // --------------------------------------------------

    #region Constructors

    /// <summary>
    /// Instantiates a new instance of the ClassReferenceDb class.
    /// </summary>
    public ClassReferenceDb()
    {
    }

    #endregion
}
