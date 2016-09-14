namespace Gu.Wpf.Media
{
    using System.Windows;

    public static class Geometries
    {
        public static ResourceKey PlayGeometryKey { get; } = Create(nameof(PlayGeometryKey));

        public static ResourceKey PauseGeometryKey { get; } = Create(nameof(PauseGeometryKey));

        public static ResourceKey StopGeometryKey { get; } = Create(nameof(StopGeometryKey));

        public static ResourceKey RewindGeometryKey { get; } = Create(nameof(RewindGeometryKey));

        public static ResourceKey FolderOpenGeometryKey { get; } = Create(nameof(FolderOpenGeometryKey));

        public static ResourceKey FullScreenGeometryKey { get; } = Create(nameof(FullScreenGeometryKey));

        public static ResourceKey MuteGeometryKey { get; } = Create(nameof(MuteGeometryKey));

        public static ResourceKey UnMuteGeometryKey { get; } = Create(nameof(UnMuteGeometryKey));

        private static ComponentResourceKey Create(string name)
        {
            return new ComponentResourceKey(typeof(Geometries), name.Replace("Key", string.Empty));
        }
    }
}
