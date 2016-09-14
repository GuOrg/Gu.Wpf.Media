namespace Gu.Wpf.Media
{
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public static class Icon
    {
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
