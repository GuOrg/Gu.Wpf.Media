namespace Gu.Wpf.Media.UiTests.Helpers
{
    using System;
    using System.IO;
    using System.Reflection;

    using NUnit.Framework;

    public static class Info
    {
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
    }
}
