namespace BindOpen.Extensions.Connecting
{
    /// <summary>
    /// This interfaces represents a connected service.
    /// </summary>
    public static class IBdoConnectedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        public static T WithConnector<T>(
            this T connected,
            IBdoConnector connector)
            where T : IBdoConnected
        {
            if (connected != null)
            {
                connected.Connector = connector;
            }

            return connected;
        }
    }
}