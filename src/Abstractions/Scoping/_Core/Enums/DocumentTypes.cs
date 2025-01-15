using System;

namespace BindOpen.Scoping
{
    /// <summary>
    /// This enumerates the possible kinds of script items.
    /// </summary>
    [Flags]
    public enum DocumentTypes
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Audio.
        /// </summary>
        Audio = 1 << 0,

        /// <summary>
        /// Application.
        /// </summary>
        Application = 1 << 1,

        /// <summary>
        /// Font.
        /// </summary>
        Font = 1 << 2,

        /// <summary>
        /// Image.
        /// </summary>
        Image = 1 << 3,

        /// <summary>
        /// Text.
        /// </summary>
        Text = 1 << 4,

        /// <summary>
        /// Video.
        /// </summary>
        Video = 1 << 5,

        /// <summary>
        /// Any.
        /// </summary>
        Any = Audio | Application | Font | Image | Text | Video
    }
}
