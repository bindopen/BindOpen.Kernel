﻿using System;
using System.Xml.Serialization;

namespace BindOpen.Data;

/// <summary>
/// This enumerates the possible requirement levels.
/// </summary>
[XmlType("RequirementLevels", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
[Flags]
public enum RequirementLevels
{
    /// <summary>
    /// None.
    /// </summary>
    None = 0,

    /// <summary>
    /// Undefined.
    /// </summary>
    Undefined = 0x01 << 0,

    /// <summary>
    /// Forbidden.
    /// </summary>
    Forbidden = 0x01 << 1,

    /// <summary>
    /// Optional. At least one optional item is required.
    /// </summary>
    Optional = 0x01 << 2,

    /// <summary>
    /// Required item.
    /// </summary>
    Required = 0x01 << 3,

    /// <summary>
    /// Custom.
    /// </summary>
    Custom = 0x01 << 4,

    /// <summary>
    /// Any the requirement level.
    /// </summary>
    Any = Forbidden | Optional | Required | Custom
}
