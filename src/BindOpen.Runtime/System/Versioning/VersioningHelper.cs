using System;
using System.Linq;

namespace BindOpen.System.Versioning
{

    /// <summary>
    /// This class represents a versioning helper.
    /// </summary>
    public static class VersioningHelper
    {
        /// <summary>
        /// Gets the specified version element.
        /// </summary>
        /// <param name="version">The new version to consider.</param>
        /// <param name="level">The base-0 level to consider.</param>
        /// <returns>The version element at the specified level.</returns>
        public static int GetVersionElement(String version, int level)
        {
            if (string.IsNullOrEmpty(version)) return -1;
            int[] versionElements = version.Split('.').Select(p => { int i = -1; if (int.TryParse(p, out i)) return i; else return -1; }).ToArray();
            if (level >= versionElements.Length)
                return 0;
            else
                return versionElements[level];
        }

        /// <summary>
        /// Gets the specified version.
        /// </summary>
        /// <param name="version">The new version to consider.</param>
        /// <param name="level">The level to consider.</param>
        /// <returns></returns>
        public static string GetVersion(String version, int level)
        {
            String newVersion = null;
            switch (level)
            {
                case 0:
                    newVersion = VersioningHelper.GetVersionElement(version, 0) + "." +
                        VersioningHelper.GetVersionElement(version, 1);
                    break;
                case 1:
                    newVersion =
                        VersioningHelper.GetVersionElement(version, 0) + "." +
                        VersioningHelper.GetVersionElement(version, 1);
                    break;
                case 2:
                    newVersion =
                        VersioningHelper.GetVersionElement(version, 0) + "." +
                        VersioningHelper.GetVersionElement(version, 1) + "." +
                        VersioningHelper.GetVersionElement(version, 2);
                    break;
                case 3:
                    newVersion =
                        VersioningHelper.GetVersionElement(version, 0) + "." +
                        VersioningHelper.GetVersionElement(version, 1) + "." +
                        VersioningHelper.GetVersionElement(version, 2) + "." +
                        VersioningHelper.GetVersionElement(version, 3);
                    break;
            }
            return newVersion;
        }

        /// <summary>
        /// Gets the specified version.
        /// </summary>
        /// <param name="numbers">The version section numbers to consider.</param>
        /// <returns></returns>
        public static string GetVersion(params int[] numbers)
        {
            string version = string.Empty;
            for (int i = 0; i < numbers.Length; i++)
                if (numbers[i] < 0)
                {
                    version += (i == 0 ? string.Empty : " ") + "Beta";
                    break;
                }
                else
                {
                    version += (string.IsNullOrEmpty(version) ? string.Empty : ".") + numbers[i].ToString();

                    // if the rest of digits are all 0, we stop
                    bool isTheRestZero = true;
                    for (int j = i + 1; j < numbers.Length; j++)
                        isTheRestZero &= (numbers[j] == 0);

                    if (isTheRestZero)
                    {
                        if (i == 0)
                        {
                            version += (string.IsNullOrEmpty(version) ? string.Empty : ".") + "0";
                        }
                        break;
                    }
                }

            return version;
        }

        /// <summary>
        /// Gets the specified version.
        /// </summary>
        /// <param name="numbers">The version section numbers to consider.</param>
        /// <returns></returns>
        public static string GetVersion(params string[] numbers)
        {
            return VersioningHelper.GetVersion(numbers.Select(p => { if (!int.TryParse(p, out int i)) return 0; return i; }).ToArray());
        }

        /// <summary>
        /// Indicates whether the specified new version is a major update of the specified current version considering the 
        /// specified major update level.
        /// </summary>
        /// <param name="newVersion">The new version to consider.</param>
        /// <param name="currentVersion">the current version to consider.</param>
        /// <param name="majorUpdateLevel">The major update level to consider.</param>
        /// <returns></returns>
        public static Boolean? IsVersionMajor(String newVersion, String currentVersion, int majorUpdateLevel)
        {
            majorUpdateLevel = (majorUpdateLevel < 0 ? 0 : (majorUpdateLevel > 3 ? 3 : majorUpdateLevel));
            int[] newVersionElements = newVersion.Split('.')
                .Select(p => { int.TryParse(p, out int i); return i; }).ToArray();
            int[] currentVersionElements = currentVersion.Split('.')
                .Select(p => { int.TryParse(p, out int i); return i; }).ToArray();

            Boolean? isMajor = null;
            int index = -1;
            while (((index++) < Math.Min(
                Math.Min(majorUpdateLevel, newVersionElements.Length - 1),
                currentVersionElements.Length - 1)) & (isMajor == null))
                if (newVersionElements[index] == currentVersionElements[index])
                    isMajor = null;
                else
                    isMajor = newVersionElements[index] > currentVersionElements[index];

            return isMajor;
        }


        /// <summary>
        /// Gets the incremented version considering the versioning format.
        /// </summary>
        /// <param name="currentVersion">The current version to consider.</param>
        /// <param name="versioningFormat">The versioning format to consider.</param>
        /// <param name="historicVersion">The historic version to consider.</param>
        /// <returns>The URL of the setup file of the new update. Null if there is no new update.</returns>
        public static string GetIncrementedVersion(
            String currentVersion,
            String versioningFormat,
            String historicVersion = null)
        {
            String newVersion = string.Empty;
            if (string.IsNullOrEmpty(historicVersion))
                historicVersion = currentVersion;

            int index = -1;
            string[] versionParts_Format = versioningFormat.Split('.');
            for (int i = 0; i < versionParts_Format.Length; i++)
                if (versionParts_Format[i] == "*")
                {
                    index = i;
                    break;
                }
            if (index > 0)
            {
                Boolean? isVersionMajor = VersioningHelper.IsVersionMajor(currentVersion, historicVersion, index - 1);
                if ((isVersionMajor != null) && (isVersionMajor.Value))
                {
                    newVersion = VersioningHelper.GetVersion(currentVersion, index - 1);
                    for (int j = index; j < versionParts_Format.Length; j++)
                        newVersion += ".0";
                }
                else if (isVersionMajor == null)
                {
                    newVersion = VersioningHelper.GetVersion(historicVersion, index - 1);
                    newVersion += "." + (VersioningHelper.GetVersionElement(historicVersion, index) + 1);
                    for (int j = index + 1; j < versionParts_Format.Length; j++)
                        newVersion += "." + (VersioningHelper.GetVersionElement(historicVersion, j) + 1);
                }
                else
                    newVersion = currentVersion;
            }

            return newVersion;
        }

    }
}
