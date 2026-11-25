using BindOpen.Data.Meta;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BindOpen.Data.Conditions;

/// <summary>
/// This class represents a condition database entity.
/// </summary>
public class ConditionDb : IBdoDb, IIdentified
{
    // ------------------------------------------
    // PROPERTIES
    // ------------------------------------------

    #region Properties

    /// <summary>
    /// The identifier of this instance.
    /// </summary>
    [Key]
    [Column("ConditionId")]
    public string Identifier { get; set; }

    /// <summary>
    /// The kind of this instance.
    /// </summary>
    [Column("ConditionKind")]
    public BdoConditionKind Kind { get; set; }

    /// <summary>
    /// The name of this instance.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The parent identifier of this instance.
    /// </summary>
    public string ParentId { get; set; }

    // Basic

    /// <summary>
    /// The arugment 1 of this instance.
    /// </summary>
    [ForeignKey(nameof(ArgumentMetaData1Id))]
    public MetaDataDb ArgumentMetaData1 { get; set; }

    /// <summary>
    /// The class name of this instance.
    /// </summary>
    public string ArgumentMetaData1Id { get; set; }

    /// <summary>
    /// The operator of this instance.
    /// </summary>
    public DataOperators Operator { get; set; }

    /// <summary>
    /// The arugment 2 of this instance.
    /// </summary>
    [ForeignKey(nameof(ArgumentMetaData2Id))]
    public MetaDataDb ArgumentMetaData2 { get; set; }

    /// <summary>
    /// The class name of this instance.
    /// </summary>
    public string ArgumentMetaData2Id { get; set; }

    // Composite

    /// <summary>
    /// The composite kind of this instance.
    /// </summary>
    public BdoCompositeConditionKind CompositionKind { get; set; } = BdoCompositeConditionKind.And;

    /// <summary>
    /// THe conditions of this instance.
    /// </summary>
    [ForeignKey(nameof(ParentId))]
    public List<ConditionDb> Conditions { get; set; }

    // Expression

    /// <summary>
    /// The expression of this instance.
    /// </summary>
    [ForeignKey(nameof(ExpressionItemId))]
    public ExpressionDb ExpressionItem { get; set; }

    /// <summary>
    /// The expression of this instance.
    /// </summary>
    public string ExpressionItemId { get; set; }

    #endregion

    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Instantiates a new instance of the ConditionDb class.
    /// </summary>
    public ConditionDb() : base()
    {
    }

    #endregion
}