namespace Gu.Wpf.Media.UiTests.Helpers
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;

    using NUnit.Framework;

    public static class Info
    {
        public static ProcessStartInfo ProcessStartInfo
        {
            get
            {
                var fileName = GetExeFileName();
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };
                return processStartInfo;
            }
        }

        internal static ProcessStartInfo CreateStartInfo(string args)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = GetExeFileName(),
                Arguments = args,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            return processStartInfo;
        }

        internal static string TestAssemblyFullFileName()
        {
            return new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
        }

        internal static string TestAssemblyDirectory() => Path.GetDirectoryName(TestAssemblyFullFileName());

        internal static string CoffeeClipFileName()
        {
            var fileName = Path.Combine(TestAssemblyDirectory(), "Samples", "coffee.mp4");
            Assert.AreEqual(true, File.Exists(fileName), "Could not find coffee clip.");
            return fileName;
        }

        private static string GetExeFileName()
        {
            //// ReSharper disable once PossibleNullReferenceException
            var fileName = Path.GetFileNameWithoutExtension(TestAssemblyFullFileName()).Replace("UiTests", "Demo");
            //// ReSharper disable once AssignNullToNotNullAttribute
            var fullFileName = Path.Combine(TestAssemblyDirectory(), fileName + ".exe");
            Assert.AreEqual(true, File.Exists(fullFileName), "Could not find demo exe.");
            return fullFileName;
        }
    }
}
