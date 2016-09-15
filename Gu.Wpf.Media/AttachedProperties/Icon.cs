namespace Gu.Wpf.Media
{
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    /// <summary>
    /// Defines attached properties for setting geometry on buttons.
    /// </summary>
    public static class Icon
    {
        /// <summary>
        /// This is an attached property.
        /// By setting a geometry on a button it can then be rendered using a style that uses the geometry in the control template.
        /// </summary>
        public static readonly DependencyProperty GeometryProperty = DependencyProperty.RegisterAttached(
            "Geometry",
            typeof(Geometry),
            typeof(Icon),
            new PropertyMetadata(default(Geometry)));

        public static void SetGeometry(this ButtonBase element, Geometry value)
        {
            element.SetValue(GeometryProperty, value);
        }

        [AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
        [AttachedPropertyBrowsableForType(typeof(ButtonBase))]
        public static Geometry GetGeometry(this ButtonBase element)
        {
            return (Geometry)element.GetValue(GeometryProperty);
        }
    }
}
