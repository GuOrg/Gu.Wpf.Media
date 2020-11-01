namespace Gu.Wpf.Media
{
    using System.ComponentModel;
    using System.Windows;

    internal static class Is
    {
        private static readonly DependencyObject DependencyObject = new DependencyObject();

        /// <summary>
        /// Determines whether the current code is executed in a design time environment such as Visual Studio or Blend.
        /// </summary>
        internal static bool InDesignMode => DesignerProperties.GetIsInDesignMode(DependencyObject);
    }
}
