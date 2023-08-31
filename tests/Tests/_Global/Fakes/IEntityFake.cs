using BindOpen.System.Data.Meta;
using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Scoping;
using System;
using System.ComponentModel;

namespace BindOpen.System.Tests
{
    /// <summary>
    /// This class represents a database data field.
    /// </summary>
    public interface IEntityFake
    {
        /// <summary>
        /// The boolean value of this instance.
        /// </summary>
        [BdoProperty("boolValue", "b", "bValue")]
        [BdoProperty(Description = "This is a boolean property")]
        [BdoProperty(RequirementLevels.Optional, @"$eq($this(""stringValue""), ""AA"")")]
        [BdoProperty(0, 1)]
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
        /// Enumeration value of this instance.
        /// </summary>
        [BdoProperty(Name = "enumValue")]
        ActionPriorities EnumValue { get; set; }

        /// <summary>
        /// Enumeration value of this instance.
        /// </summary>
        [BdoProperty(Name = "inputs")]
        [BdoFunction(Name = "input")]
        BdoMetaNode Inputs { get; set; }

        /// <summary>
        /// The sub entity of this instance.
        /// </summary>
        [BdoProperty(Name = "subEntity")]
        EntityFake SubEntity { get; set; }

        // Path --------------------------

        /// <summary>
        /// Path of this instance.
        /// </summary>
        [BdoProperty("path")]
        string Path { get; set; }

        /// <summary>
        /// The relative path of this instance.
        /// </summary>
        string RelativePath { get; set; }

        /// <summary>
        /// The creation date of this instance.
        /// </summary>
        [BdoProperty("creationDate")]
        DateTime? CreationDate { get; set; }

        /// <summary>
        /// The information flag of this instance.
        /// </summary>
        [BdoProperty("flag")]
        string Flag { get; set; }

        /// <summary>
        /// Indicates whether this instance is read only.
        /// </summary>
        [BdoProperty("isReadOnly")]
        [DefaultValue(false)]
        bool IsReadonly { get; set; }

        /// <summary>
        /// The date of last access of this instance.
        /// </summary>
        [BdoProperty("lastAccessDate")]
        DateTime? LastAccessDate { get; set; }

        /// <summary>
        /// The parent path of this instance.
        /// </summary>
        [BdoProperty("parentPath")]
        string ParentPath { get; set; }

        /// <summary>
        /// The date of last write of this instance.
        /// </summary>
        [BdoProperty("lastWriteDate")]
        DateTime? LastWriteDate { get; set; }
    }
}
