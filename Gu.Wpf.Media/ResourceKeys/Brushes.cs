#pragma warning disable SA1600
#pragma warning disable 1591
namespace Gu.Wpf.Media
{
    using System.Runtime.CompilerServices;
    using System.Windows;

    /// <summary>
    /// A set of resource keys for brushes.
    /// </summary>
    public static class Brushes
    {
        public static ResourceKey EnabledForegroundBrushKey { get; } = Create();

        public static ResourceKey DisabledForegroundBrushKey { get; } = Create();

        public static ResourceKey BackgroundBrushKey { get; } = Create();

        public static ResourceKey SemiTransparentBackgroundBrushKey { get; } = Create();

        private static ComponentResourceKey Create([CallerMemberName]string? name = null)
        {
            return new ComponentResourceKey(typeof(Brushes), name.TrimEnd("Key"));
        }
    }
}