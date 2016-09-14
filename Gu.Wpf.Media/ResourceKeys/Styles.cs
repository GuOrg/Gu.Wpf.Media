namespace Gu.Wpf.Media
{
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class Styles
    {
        public static ResourceKey PlayerButtonBaseStyleKey { get; } = Create();

        public static ResourceKey ProgressRepeatButtonStyleKey { get; } = Create();

        public static ResourceKey ProgressThumbStyleKey { get; } = Create();

        public static ResourceKey ProgressSliderStyleKey { get; } = Create();

        private static ComponentResourceKey Create([CallerMemberName]string name = null)
        {
            return new ComponentResourceKey(typeof(Styles), name.TrimEnd("Key"));
        }
    }
}
