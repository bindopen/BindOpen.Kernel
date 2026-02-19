using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Scoping.Entities;
using System;
using System.ComponentModel;

namespace BindOpen.Scoping.Tests;

/// <summary>
/// This class represents a fake database entity used to test complex property mapping,
/// nested nodes, and conditional script-based validation rules.
/// </summary>
[BdoEntity(
    Name = "testEntity",
    Description = "Database field simulation.",
    CreationDate = "2016-09-14"
)]
public class EntityFake : BdoEntity, IEntityFake
{
    /// <summary>
    /// The unique extension name used by the scoping engine.
    /// </summary>
    public static string __DefinitionUniqueName = "scoping.tests$testEntity";

    // -----------------------------------------------
    // VARIABLES
    // -----------------------------------------------

    #region Variables

    private string _relativePath = null;

    #endregion

    // ------------------------------------------
    // PROPERTIES
    // ------------------------------------------

    #region Properties

    /// <summary>
    /// A boolean value with multiple aliases and a conditional requirement.
    /// It is required only if 'stringValue' equals 'AA'.
    /// </summary>
    [BdoProperty("boolValue", "b", "bValue")]
    [BdoProperty(Description = "This is a boolean property with aliases.")]
    [BdoProperty(RequirementLevels.Optional, @"$eq($this(""stringValue""), ""AA"")")]
    public ITBdoMetaScalar<bool?> BoolValue { get; set; }

    /// <summary>
    /// A simple string property.
    /// </summary>
    [BdoProperty(Name = "stringValue")]
    public string StringValue { get; set; }

    /// <summary>
    /// A simple integer property.
    /// </summary>
    [BdoProperty(Name = "intValue")]
    public int IntValue { get; set; }

    /// <summary>
    /// An enumeration property mapping AccessibilityLevels.
    /// </summary>
    [BdoProperty(Name = "enumValue")]
    public AccessibilityLevels EnumValue { get; set; }

    /// <summary>
    /// A complex metadata node property. Also accessible as a function.
    /// </summary>
    [BdoProperty(Name = "inputs")]
    [BdoFunction(Name = "input")]
    public BdoMetaNode Inputs { get; set; }

    /// <summary>
    /// A recursive property allowing for sub-entities.
    /// </summary>
    [BdoProperty(Name = "subEntity")]
    public EntityFake SubEntity { get; set; }

    // Path Management --------------------------

    /// <summary>
    /// The primary path of this entity.
    /// </summary>
    [BdoProperty("path")]
    public string Path { get; set; }

    /// <summary>
    /// Gets or sets the relative path, automatically updating the primary Path.
    /// </summary>
    public string RelativePath
    {
        get => _relativePath;
        set => WithPath(null, value);
    }

    /// <summary>
    /// Updates the path and relative path of this instance.
    /// </summary>
    public virtual IBdoEntity WithPath(string path = null, string relativePath = null)
    {
        string absolutePath = path ?? Path;

        if (!string.IsNullOrEmpty(relativePath))
        {
            _relativePath = relativePath;
        }

        if (!string.IsNullOrEmpty(_relativePath) && !string.IsNullOrEmpty(absolutePath))
        {
            if (absolutePath.StartsWith(_relativePath))
            {
                absolutePath = absolutePath[_relativePath.Length..];
            }
        }

        Path = absolutePath;
        return this;
    }

    // Meta Metadata --------------------------

    [BdoProperty("creationDate")]
    public DateTime? CreationDate { get; set; }

    [BdoProperty("flag")]
    public string Flag { get; set; }

    public IBdoEntity WithFlag(string flag)
    {
        Flag = flag;
        return this;
    }

    [BdoProperty("isReadOnly")]
    [DefaultValue(false)]
    public bool IsReadonly { get; set; }

    public IBdoEntity AsReadonly(bool readOnly = false)
    {
        IsReadonly = readOnly;
        return this;
    }

    [BdoProperty("lastAccessDate")]
    public DateTime? LastAccessDate { get; set; }

    public IBdoEntity WithLastAccessDate(DateTime? date)
    {
        LastAccessDate = date;
        return this;
    }

    [BdoProperty("parentPath")]
    public string ParentPath { get; set; }

    public IBdoEntity WithParentPath(string path)
    {
        ParentPath = path;
        return this;
    }

    [BdoProperty("lastWriteDate")]
    public DateTime? LastWriteDate { get; set; }

    public IBdoEntity WithLastWriteDate(DateTime? date)
    {
        LastWriteDate = date;
        return this;
    }

    #endregion

    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    public EntityFake() : base() { }

    public EntityFake(string path) : base()
    {
        WithPath(path);
    }

    public EntityFake(string fileName, string folderPath, EntityFake subEntity = null) : base()
    {
        WithPath(fileName, folderPath);
        SubEntity = subEntity;
    }

    #endregion

    public override string ToString() => Path ?? string.Empty;
}