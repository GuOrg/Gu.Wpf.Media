namespace Gu.Wpf.Media.Demo.UiTestWindows
{
    using System.IO;

    public static class Files
    {
        public static string CoffeeClip { get; } = Path.Combine(Directory.GetCurrentDirectory(), "Samples", "coffee.mp4");
    }
}