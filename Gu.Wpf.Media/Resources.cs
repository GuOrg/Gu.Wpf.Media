namespace Gu.Wpf.Media
{
    using System.Windows;

    public static class Resources
    {
        public static ResourceKey PlayGeometryKey { get; } = Create(nameof(PlayGeometryKey));

        public static ResourceKey PauseGeometryKey { get; } = Create(nameof(PauseGeometryKey));

        public static ResourceKey StopGeometryKey { get; } = Create(nameof(StopGeometryKey));

        public static ResourceKey FolderOpenGeometryKey { get; } = Create(nameof(FolderOpenGeometryKey));

        public static ResourceKey MaximizeGeometryKey { get; } = Create(nameof(MaximizeGeometryKey));

        public static ResourceKey MuteGeometryKey { get; } = Create(nameof(MuteGeometryKey));

        public static ResourceKey UnMuteGeometryKey { get; } = Create(nameof(UnMuteGeometryKey));

        private static ComponentResourceKey Create(string name)
        {
            return new ComponentResourceKey { ResourceId = nameof(name).Replace("Key", string.Empty) };
        }
    }
}
