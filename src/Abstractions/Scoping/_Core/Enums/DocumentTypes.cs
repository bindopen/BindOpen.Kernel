using System;

namespace BindOpen.Scoping;

/// <summary>
/// Enumerates the standard types of documents and media formats.
/// </summary>
/// <remarks>
/// This enumeration supports bitwise combinations, allowing for multi-type 
/// filtering in document processing and scoping operations.
/// </remarks>
[Flags]
public enum DocumentTypes
{
    /// <summary>
    /// No document type specified.
    /// </summary>
    None = 0,

    /// <summary>
    /// Audio files and streams.
    /// </summary>
    Audio = 1 << 0,

    /// <summary>
    /// Binary files, executables, or application-specific data.
    /// </summary>
    Application = 1 << 1,

    /// <summary>
    /// Digital font files.
    /// </summary>
    Font = 1 << 2,

    /// <summary>
    /// Static images and graphics.
    /// </summary>
    Image = 1 << 3,

    /// <summary>
    /// Plain text or formatted text documents.
    /// </summary>
    Text = 1 << 4,

    /// <summary>
    /// Video files and multimedia streams.
    /// </summary>
    Video = 1 << 5,

    /// <summary>
    /// Represents a combination of all document types.
    /// </summary>
    Any = Audio | Application | Font | Image | Text | Video
}