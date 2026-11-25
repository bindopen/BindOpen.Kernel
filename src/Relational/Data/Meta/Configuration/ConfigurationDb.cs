using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BindOpen.Data.Meta;

/// <summary>
/// This class represents a config database entity.
/// </summary>
public class ConfigurationDb : MetaSetDb
{
    // -------------------------------------------------------
    // PROPERTIES
    // -------------------------------------------------------

    #region Properties

    public string ParentId { get; set; }

    /// <summary>
    /// Creation date of this instance.
    /// </summary>
    public DateTime? CreationDate { get; set; }

    /// <summary>
    /// Last modification date of this instance.
    /// </summary>
    public DateTime? LastModificationDate { get; set; }

    /// <summary>
    /// The description database entity of this instance.
    /// </summary>
    [ForeignKey(nameof(TitleStringDictionaryId))]
    public StringDictionaryDb Title { get; set; }

    /// <summary>
    /// The class name of this instance.
    /// </summary>
    public string TitleStringDictionaryId { get; set; }

    /// <summary>
    /// The description database entity of this instance.
    /// </summary>
    [ForeignKey(nameof(DescriptionStringDictionaryId))]
    public StringDictionaryDb Description { get; set; }

    /// <summary>
    /// The class name of this instance.
    /// </summary>
    public string DescriptionStringDictionaryId { get; set; }

    /// <summary>
    /// The children of this instance.
    /// </summary>
    [ForeignKey(nameof(ParentId))]
    public List<ConfigurationDb> Children { get; set; }

    /// <summary>
    /// The using item IDs of this instance.
    /// </summary>
    public List<string> UsedItemIds { get; set; }

    #endregion

    // -------------------------------------------------------------
    // CONSTRUCTORS
    // -------------------------------------------------------------

    #region Constructors

    /// <summary>
    /// Instantiates a new instance of the BdoBaseConfigurationDb class.
    /// </summary>
    public ConfigurationDb() : base()
    {
    }

    #endregion
}
