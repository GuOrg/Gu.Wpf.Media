namespace Gu.Wpf.Media
{
    using System.Windows;

    public static class Styles
    {
        public static ResourceKey PlayerButtonBaseStyleKey { get; } = Create(nameof(PlayerButtonBaseStyleKey));

        private static ComponentResourceKey Create(string name)
        {
            return new ComponentResourceKey(typeof(Geometries), name.Replace("Key", string.Empty));
        }
    }
}
