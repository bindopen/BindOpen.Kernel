using System.Xml.Serialization;

namespace BindOpen.Logging;

/// <summary>
/// Enumerates the levels of criticality for data, events, or processes.
/// </summary>
/// <remarks>
/// This scale is used to prioritize log events and determine the urgency 
/// of system notifications or automated recovery actions.
/// </remarks>
[XmlType("Criticalities", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
public enum Criticalities
{
    /// <summary>
    /// No criticality specified or not applicable.
    /// </summary>
    None,

    /// <summary>
    /// Low impact: Minimal effect on system performance or data integrity.
    /// </summary>
    Low,

    /// <summary>
    /// Medium impact: Significant but non-blocking issues.
    /// </summary>
    Medium,

    /// <summary>
    /// High impact: Serious issues that may lead to partial system failure.
    /// </summary>
    High,

    /// <summary>
    /// Very High impact: Critical failure requiring immediate intervention.
    /// </summary>
    VeryHigh
};