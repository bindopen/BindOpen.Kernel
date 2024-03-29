﻿using BindOpen.Kernel.Tests;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Scoping
{
    /// <summary>
    /// This class set the global setup.
    /// </summary>
    [SetUpFixture]
    public class GlobalSetUp
    {
        /// <summary>
        /// 
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // we delete the working folder

            if (Directory.Exists(SystemData.WorkingFolder))
            {
                Directory.Delete(SystemData.WorkingFolder, true);
            }
        }
    }
}