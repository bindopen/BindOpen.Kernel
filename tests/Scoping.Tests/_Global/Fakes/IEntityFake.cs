using BindOpen.Data;
using BindOpen.Data.Meta;
using System;
using System.ComponentModel;

namespace BindOpen.Scoping.Tests;

/// <summary>
/// This interface defines the contract for fake database entities, including 
/// the metadata mapping rules used by the BindOpen scoping engine.
/// </summary>
public interface IEntityFake
{
    // ------------------------------------------
    // PROPERTIES
    // ------------------------------------------

    #region Properties

    /// <summary>
    /// The boolean value of this instance. 
    /// Supports multiple aliases ("b", "bValue") and conditional requirements via script.
    /// </summary>
    [BdoProperty("boolValue", "b", "bValue")]
    [BdoProperty(Description = "This is a boolean property")]
    [BdoProperty(RequirementLevels.Optional, @"$eq($this(""stringValue""), ""AA"")")]
    [BdoProperty(SpecificationLevels.Configuration, SpecificationLevels.Definition)]
    ITBdoMetaScalar<bool?> BoolValue { get; set; }

    /// <summary>
    /// The string value of this instance.
    /// </summary>
    [BdoProperty(Name = "stringValue")]
    string StringValue { get; set; }

    /// <summary>
    /// The integer value of this instance.
    /// </summary>
    [BdoProperty(Name = "intValue")]
    int IntValue { get; set; }

    /// <summary>
    /// Enumeration value of this instance mapping AccessibilityLevels.
    /// </summary>
    [BdoProperty(Name = "enumValue")]
    AccessibilityLevels EnumValue { get; set; }

    /// <summary>
    /// Metadata node representing inputs. Also accessible via the 'input' script function.
    /// </summary>
    [BdoProperty(Name = "inputs")]
    [BdoFunction(Name = "input")]
    BdoMetaNode Inputs { get; set; }

    /// <summary>
    /// A sub-entity property demonstrating nested object hierarchies.
    /// </summary>
    [BdoProperty(Name = "subEntity")]
    EntityFake SubEntity { get; set; }

    // Path Management --------------------------

    /// <summary>
    /// The primary path of the instance.
    /// </summary>
    [BdoProperty("path")]
    string Path { get; set; }

    /// <summary>
    /// The relative path of the instance.
    /// </summary>
    string RelativePath { get; set; }

    // Metadata Tracking --------------------------

    /// <summary>
    /// The creation date of this instance.
    /// </summary>
    [BdoProperty("creationDate")]
    DateTime? CreationDate { get; set; }

    /// <summary>
    /// Information flag for categorization or status.
    /// </summary>
    [BdoProperty("flag")]
    string Flag { get; set; }

    /// <summary>
    /// Indicates if the entity is read-only. Defaults to false.
    /// </summary>
    [BdoProperty("isReadOnly")]
    [DefaultValue(false)]
    bool IsReadonly { get; set; }

    /// <summary>
    /// Timestamp of the last time this entity was accessed.
    /// </summary>
    [BdoProperty("lastAccessDate")]
    DateTime? LastAccessDate { get; set; }

    /// <summary>
    /// The parent path for hierarchical organization.
    /// </summary>
    [BdoProperty("parentPath")]
    string ParentPath { get; set; }

    /// <summary>
    /// Timestamp of the last time this entity was modified.
    /// </summary>
    [BdoProperty("lastWriteDate")]
    DateTime? LastWriteDate { get; set; }

    #endregion
}