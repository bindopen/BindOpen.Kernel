using dkm.standard.extension.carriers;
using dkm.standard.extension.carriers.file;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace dkm.standard.data.repository.ftp
{

    /// <summary>
    /// This class represents a tool to help to deal with ftp connection parsing issues.
    /// </summary>
    public class FtpConnectionParser
    {

        // ---------------------------------
        // CONSTANTS
        // ---------------------------------

        #region Constants

        static char myReplacementChar = '_';
        const String myUnixSymbolicLinkPathSeparator = " -> ";

        #endregion


        // ---------------------------------
        // PUBLIC FUNCTIONS
        // ---------------------------------

        #region Public functions

        /// <summary>
        /// Gets the cleaned name of the specified file name.
        /// </summary>
        /// <param name="aFileName">The file name to consider.</param>
        /// <returns></returns>
        public static String GetCleanedFileName(String aFileName)
        {
            return FtpConnectionParser.ReplaceAllChars(aFileName, Path.GetInvalidFileNameChars(), myReplacementChar);
        }

        /// <summary>
        /// Gets the list of elements from the specified data string.
        /// </summary>
        /// <param name="aDataString">The data string to consider.</param>
        /// <returns>Returns the list of elements from the specified data string.</returns>
        public static IList<RepositoryFile> GetRepositoryFileList(String aDataString)
        {
            try
            {
                List<RepositoryFile> someRepositoryFiles = new List<RepositoryFile>();
                String[] someLines = aDataString.Split('\n');
                RepositoryFile.RepositoryFileListingStyle aRepositoryFileListingStyle = FtpConnectionParser.DetectElementListingStyle(someLines);
                foreach (String aLine in someLines)
                    if ((aRepositoryFileListingStyle != RepositoryFile.RepositoryFileListingStyle.Unknown) && (aLine != ""))
                    {
                        RepositoryFile aRepositoryFile = new RepositoryFile();
                        aRepositoryFile.Name = "..";
                        switch (aRepositoryFileListingStyle)
                        {
                            case RepositoryFile.RepositoryFileListingStyle.Unix:
                                aRepositoryFile = FtpConnectionParser.ParseRepositoryFileFromUnixStyleLine(aLine);
                                break;
                            case RepositoryFile.RepositoryFileListingStyle.Windows:
                                aRepositoryFile = FtpConnectionParser.ParseRepositoryFileFromWindowsStyleLine(aLine);
                                break;
                        }

                        if (!(aRepositoryFile == null || aRepositoryFile.Name == "." || aRepositoryFile.Name == ".."))
                            someRepositoryFiles.Add(aRepositoryFile);
                    }

                return someRepositoryFiles;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to determine the folder items.", ex);
            }
        }

        #endregion


        // ---------------------------------
        // PRIVATE FUNCTIONS
        // ---------------------------------

        #region Private functions

        private static String ReplaceAllChars(String st, char[] someOldChars, char aNewChar)
        {
            foreach (char aOldChar in someOldChars)
                st.Replace(aOldChar, aNewChar);
            return st;
        }

        private static RepositoryFile.RepositoryFileListingStyle DetectElementListingStyle(String[] someLines)
        {
            foreach (String st in someLines)
                if (st.Length > 10 && Regex.IsMatch(st.Substring(0, 10), "(-|d)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)"))
                    return RepositoryFile.RepositoryFileListingStyle.Unix;
                else if (st.Length > 8 && Regex.IsMatch(st.Substring(0, 8), "[0-9][0-9]-[0-9][0-9]-[0-9][0-9]"))
                    return RepositoryFile.RepositoryFileListingStyle.Windows;

            return RepositoryFile.RepositoryFileListingStyle.Unknown;
        }

        private static RepositoryFile ParseRepositoryFileFromWindowsStyleLine(String aLine)
        {
            // We consider the line is this way
            // 02-03-04  07:46PM       <DIR>          Append

            RepositoryFile aRepositoryFile = new RepositoryFile();
            aLine = aLine.Trim();

            String aDate = aLine.Substring(0, 8);
            aLine = (aLine.Substring(8, aLine.Length - 8)).Trim();
            String aTime = aLine.Substring(0, 7);
            aLine = (aLine.Substring(7, aLine.Length - 7)).Trim();
            aRepositoryFile.CreationTime = DateTime.Parse(aDate + " " + aTime, CultureInfo.GetCultureInfo("en-US"));

            if (aLine.Substring(0, 5) == "<DIR>")
            {
                aRepositoryFile.Kind = RepositoryFile.RepositoryFileKind.Folder;
                aLine = (aLine.Substring(5, aLine.Length - 5)).Trim();
            }
            else
            {
                aRepositoryFile.Kind = RepositoryFile.RepositoryFileKind.File;

                int i = aLine.IndexOf(' ');
                aRepositoryFile.Length = ulong.Parse(aLine.Substring(0, i));
                aLine = aLine.Substring(i + 1);
            }
            aRepositoryFile.Name = aLine;
            return aRepositoryFile;
        }

        private static RepositoryFile ParseRepositoryFileFromUnixStyleLine(String aLine)
        {
            // We consider the line is this way
            // dr-xr-xr-x   1 owner    group               0 Nov 25  2002 bussys
            // Mac OS X - tnftpd returns the total on the first line

            if (aLine.ToLower().StartsWith("total "))
                return null;

            RepositoryFile aRepositoryFile = new RepositoryFile();
            aLine = aLine.Trim();
            aRepositoryFile.Flag = aLine.Substring(0, 9);
            aRepositoryFile.Kind = (aRepositoryFile.Flag[0] == 'd' ? RepositoryFile.RepositoryFileKind.Folder : RepositoryFile.RepositoryFileKind.File);
            aRepositoryFile.IsSymbolicLink = (aRepositoryFile.Flag[0] == 'l');                

            aLine = (aLine.Substring(11)).Trim();
            FtpConnectionParser.CutTrimedSubstringFromString(ref aLine, " ", 0);
            FtpConnectionParser.CutTrimedSubstringFromString(ref aLine, " ", 0);  // Owner
            FtpConnectionParser.CutTrimedSubstringFromString(ref aLine, " ", 0);  // Group
            aRepositoryFile.Length = ulong.Parse(FtpConnectionParser.CutTrimedSubstringFromString(ref aLine, " ", 0));  
            
            String aTime = CutTrimedSubstringFromString(ref aLine, " ", 8);
            if (aTime[4] == ' ') aTime = aTime.Substring(0, 4) + "0" + aTime.Substring(5);

            aRepositoryFile.CreationTime = DateTime.ParseExact(
                aTime,
                (aTime.IndexOf(':') < 0 ? "MMM dd yyyy" : "MMM dd H:mm"), 
                CultureInfo.GetCultureInfo("en-US"), 
                DateTimeStyles.AllowWhiteSpaces);

            if ((aRepositoryFile.IsSymbolicLink) && (aLine.IndexOf(myUnixSymbolicLinkPathSeparator) > 0))
            {
                aRepositoryFile.Name = CutTrimedSubstringFromString(ref aLine, myUnixSymbolicLinkPathSeparator, 0);
                aRepositoryFile.SymbolicLinkPath = aLine;
            }
            else
                aRepositoryFile.Name = aLine;

            return aRepositoryFile;
        }

        private static String CutTrimedSubstringFromString(ref String st, String aSubString, int aStartIndex)
        {
            int aPos = st.IndexOf(aSubString, aStartIndex);
            String aString = st.Substring(0, aPos);
            st = (st.Substring(aPos + aSubString.Length)).Trim();

            return aString;
        }

        #endregion
    }
}
