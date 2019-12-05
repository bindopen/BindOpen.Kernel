using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Data.Items.Dto;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Helpers.Files
{
    /// <summary>
    /// This class represents a helper for files.
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Creates the specified file directory if it does not exist.
        /// </summary>
        /// <param name="folderPath">The path of the file directory to consider.</param>
        /// <param name="log">The log to append.</param>
        /// <returns>Returns True whether the directory exists henceforth. False otherwise.</returns>
        public static bool CreateDirectory(string folderPath, IBdoLog log = null)
        {
            var isExisting = false;
            try
            {
                // we create the folder if it does not exist
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                isExisting = true;
            }
            catch(Exception ex)
            {
                log?.AddException(ex);
            }

            return isExisting;
        }
    }
}
