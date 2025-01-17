﻿using System.Xml.Serialization;

namespace BindOpen.Data;

/// <summary>
/// This enumerates the possible data source kinds.
/// </summary>
[XmlType("DatasourceKind", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
public enum DatasourceKind
{
    /// <summary>
    /// None.
    /// </summary>
    None,

    /// <summary>
    /// Any.
    /// </summary>
    Any,

    /// <summary>
    /// Database.
    /// </summary>
    Database,

    /// <summary>
    /// Repository.
    /// </summary>
    Repository,

    /// <summary>
    /// Rest API.
    /// </summary>
    RestApi,

    /// <summary>
    /// SOAP API.
    /// </summary>
    SoapApi,

    /// <summary>
    /// Email server.
    /// </summary>
    EmailServer,

    /// <summary>
    /// Memory.
    /// </summary>
    Memory,

    /// <summary>
    /// Console.
    /// </summary>
    Console
}
