namespace BindOpen.System.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoLoggedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static T WithLog<T>(
            this T logged,
            IBdoLog log)
            where T : IBdoLogged
        {
            if (logged != null)
            {
                logged.Log = log;
            }

            return logged;
        }
    }
}