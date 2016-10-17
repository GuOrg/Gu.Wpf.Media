namespace Gu.Wpf.Media.Demo.UiTestWindows
{
    using System;
    using System.IO;
    using System.Reflection;

    public static class Files
    {
        public static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public static string CoffeeClip { get; } = Path.Combine(AssemblyDirectory, "Samples", "coffee.mp4");
    }
}