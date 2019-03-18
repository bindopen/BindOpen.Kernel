using System;

namespace BindOpen.Framework.Core.System.Helpers
{

    /// <summary>
    /// This class represents a instance of system helper.
    /// </summary>
    public static class SystemHelper
    {

        /// <summary>
        /// Returns the current time stamp.
        /// </summary>
        /// <returns>The current time stamp</returns>
        public static String GetCurrentTimeStamp()
        {
            return global::System.DateTime.Now.Year +
                (global::System.DateTime.Now.Month < 10 ? "0" + global::System.DateTime.Now.Month.ToString() : global::System.DateTime.Now.Month.ToString()) +
                (global::System.DateTime.Now.Day < 10 ? "0" + global::System.DateTime.Now.Day.ToString() : global::System.DateTime.Now.Day.ToString()) +
                (global::System.DateTime.Now.Hour < 10 ? "0" + global::System.DateTime.Now.Hour.ToString() : global::System.DateTime.Now.Hour.ToString()) +
                (global::System.DateTime.Now.Minute < 10 ? "0" + global::System.DateTime.Now.Minute.ToString() : global::System.DateTime.Now.Minute.ToString()) +
                (global::System.DateTime.Now.Second < 10 ? "0" + global::System.DateTime.Now.Second.ToString() : global::System.DateTime.Now.Second.ToString());
        }


    }
}