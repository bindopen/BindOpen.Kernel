﻿using System;
using System.Xml.Serialization;

namespace BindOpen.Data;

/// <summary>
/// This enumerates the possible label formats.
/// </summary>
[Flags]
[XmlType("LabelFormats", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
public enum LabelFormats
{
    /// <summary>
    /// Any.
    /// </summary>
    Any,

    /// <summary>
    /// Only name.
    /// </summary>
    OnlyName,

    /// <summary>
    /// Name=value.
    /// </summary>
    NameEqualsValue,

    /// <summary>
    /// Name:value.
    /// </summary>
    NameColonValue,

    /// <summary>
    /// Name value.
    /// </summary>
    NameSpaceValue,

    /// <summary>
    /// Only value.
    /// </summary>
    OnlyValue,
}