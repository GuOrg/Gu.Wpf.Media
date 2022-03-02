namespace Gu.Wpf.Media.Demo.UiTestWindows
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class PropertyItem : INotifyPropertyChanged
    {
        private static readonly Dictionary<DependencyProperty, PropertyItem> Cache = new();

        private static readonly FieldInfo MediaElementField = typeof(MediaElementWrapper).GetField("mediaElement", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        private readonly MediaElementWrapper wrapper;
        private readonly MediaElement mediaElement;
        private readonly PropertyInfo mediaElementProperty;

        private readonly DependencyProperty proxyProperty;

        private PropertyItem(MediaElementWrapper wrapper, DependencyProperty property, DependencyProperty proxyProperty)
        {
            this.wrapper = wrapper;
            this.mediaElement = (MediaElement)MediaElementField.GetValue(wrapper);
            this.Property = property;
            this.mediaElementProperty = typeof(MediaElement).GetProperty(property.Name);
            this.proxyProperty = proxyProperty;
            if (property.ReadOnly)
            {
                var binding = new Binding(property.Name) { Source = wrapper, Mode = BindingMode.OneWay };
                _ = BindingOperations.SetBinding(wrapper, proxyProperty, binding);
            }
            else
            {
                var binding = new Binding(property.Name) { Source = wrapper, Mode = BindingMode.TwoWay };
                _ = BindingOperations.SetBinding(wrapper, proxyProperty, binding);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public DependencyProperty Property { get; }

        public object Value
        {
            get => this.wrapper.GetValue(this.proxyProperty);

            set
            {
                this.wrapper.SetValue(this.proxyProperty, value);
                this.OnPropertyChanged();
            }
        }

        public object MediaElementWrapperValue => this.wrapper.GetValue(this.Property) ?? "null";

        public object MediaElementValue => this.mediaElementProperty?.GetValue(this.mediaElement) ?? "-";

        public static PropertyItem GetOrCreate(MediaElementWrapper wrapper, DependencyProperty property)
        {
            if (!Cache.TryGetValue(property, out PropertyItem item))
            {
                var proxy = DependencyProperty.RegisterAttached(
                    property.Name + "Proxy",
                    typeof(object),
                    typeof(PropertyItem),
                    new PropertyMetadata(null, OnProxyPropertyChanged));
                item = new PropertyItem(wrapper, property, proxy);
                Cache[property] = item;
            }

            return item;
        }

        public override string ToString()
        {
            return $"{nameof(this.Property)}: {this.Property}, {nameof(this.Value)}: {this.Value}";
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static void OnProxyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            foreach (var kvp in Cache)
            {
                var item = kvp.Value;
                item.OnPropertyChanged(nameof(Value));
                item.OnPropertyChanged(nameof(MediaElementWrapperValue));
                if (item.mediaElementProperty != null)
                {
                    item.OnPropertyChanged(nameof(item.MediaElementValue));
                }
            }
        }
    }
}
