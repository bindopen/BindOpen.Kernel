    /// <summary>
    /// This enumeration lists the possible criticalities.
    /// </summary>
    export enum BdoConstraintModes
    {
        /// <summary>
        /// None.
        /// </summary>
        None = "None",

        Any = "Any",

        /// <summary>
        /// Element must be.
        /// </summary>
        Requirement = "Requirement",

        /// <summary>
        /// Element must not be.
        /// </summary>
        Rule = "Rule"
    };
