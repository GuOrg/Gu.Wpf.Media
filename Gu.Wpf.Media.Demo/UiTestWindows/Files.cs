namespace Gu.Wpf.Media.Demo.UiTestWindows
{
    using System.IO;
    using System.Reflection;

    public static class Files
    {
        public static string AssemblyDirectory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location!)!;

        public static string CoffeeClip { get; } = Path.Combine(AssemblyDirectory, "Samples", "coffee.mp4");
    }
}
