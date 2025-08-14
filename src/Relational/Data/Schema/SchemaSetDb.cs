using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BindOpen.Data.Schema;

/// <summary>
/// This class represents a meta set database entity.
/// </summary>
public class SchemaSetDb : IBdoDb
{
    // -------------------------------------------------------
    // PROPERTIES
    // -------------------------------------------------------

    #region Properties

    /// <summary>
    /// ID of this instance.
    /// </summary>
    [Key]
    [Column("SpecSetId")]
    public string Identifier { get; set; }

    /// <summary>
    /// Name of this instance.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The elements of this instance.
    /// </summary>
    [NotMapped]
    public List<SchemaDb> Items { get; set; }

    #endregion

    // -------------------------------------------------------------
    // CONSTRUCTORS
    // -------------------------------------------------------------

    #region Constructors

    /// <summary>
    /// Instantiates a new instance of the SpecSetDb class.
    /// </summary>
    public SchemaSetDb() : base()
    {
    }

    #endregion
}
