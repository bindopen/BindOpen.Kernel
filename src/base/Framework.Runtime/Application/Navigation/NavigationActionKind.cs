namespace BindOpen.Framework.Runtime.Application.Navigation
{
    /// <summary>
    /// This enumeration lists the possible kinds of user actions.
    /// </summary>
    public enum NavigationActionKind
    {
        /// <summary>
        /// All actions. It is represented by the '*' character.
        /// </summary>
        All,

        /// <summary>
        /// Log as another user.
        /// </summary>
        LogAs,

        /// <summary>
        /// Make a quick search an entity object.
        /// </summary>
        QuickSearch,

        /// <summary>
        /// Make a basic search an entity object.
        /// </summary>
        BasicSearch,

        /// <summary>
        /// Advanced search an entity object.
        /// </summary>
        AdvancedSearch,

        /// <summary>
        /// Add an entity object.
        /// </summary>
        Add,

        /// <summary>
        /// Duplicate an entity object.
        /// </summary>
        Duplicate,

        /// <summary>
        /// View properties of an entity object.
        /// </summary>
        Properties,

        /// <summary>
        /// Refresh.
        /// </summary>
        Refresh,

        /// <summary>
        /// Edit an entity object.
        /// </summary>
        Edit,

        /// <summary>
        /// Delete an entity object.
        /// </summary>
        Delete,

        /// <summary>
        /// Export an entity object.
        /// </summary>
        Export,

        /// <summary>
        /// Import an entity object.
        /// </summary>
        Import,

        /// <summary>
        /// Print an entity object.
        /// </summary>
        Print,

        /// <summary>
        /// Go backward.
        /// </summary>
        Back,

        /// <summary>
        /// Insert an entity object.
        /// </summary>
        Insert,

        /// <summary>
        /// Cancel when inserting an entity object.
        /// </summary>
        InsertCancel,

        /// <summary>
        /// Save properties of an entity object.
        /// </summary>
        Save,

        /// <summary>
        /// Cancel when saving properties of an entity object.
        /// </summary>
        SaveCancel,

        /// <summary>
        /// Custom action.
        /// </summary>
        Custom
    }
}
