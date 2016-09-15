#pragma warning disable SA1600 // Elements must be documented
#pragma warning disable 1591
namespace Gu.Wpf.Media
{
    using System.Runtime.CompilerServices;
    using System.Windows;

    /// <summary>
    /// A set of resource keys for geometries.
    /// </summary>
    public static class Geometries
    {
        public static ResourceKey PlayGeometryKey { get; } = Create();

        public static ResourceKey PauseGeometryKey { get; } = Create();

        public static ResourceKey StopGeometryKey { get; } = Create();

        public static ResourceKey RewindGeometryKey { get; } = Create();

        public static ResourceKey FolderOpenGeometryKey { get; } = Create();

        public static ResourceKey FullScreenGeometryKey { get; } = Create();

        public static ResourceKey MuteGeometryKey { get; } = Create();

        public static ResourceKey UnMuteGeometryKey { get; } = Create();

        private static ComponentResourceKey Create([CallerMemberName]string name = null)
        {
            return new ComponentResourceKey(typeof(Geometries), name.TrimEnd("Key"));
        }
    }
}
