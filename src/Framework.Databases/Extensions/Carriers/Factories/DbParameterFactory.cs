using BindOpen.Framework.Core.Data.Helpers.Strings;

namespace BindOpen.Framework.Databases.Extensions.Carriers
{
    /// <summary>
    /// This static class represents a factory of database parameter.
    /// </summary>
    public static class DbParameterFactory
    {
        /// <summary>
        /// Creates a string formated of the specfied parameter.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        public static string Create(string name) => StringHelper.__UniqueToken + name + StringHelper.__UniqueToken;

    }
}
