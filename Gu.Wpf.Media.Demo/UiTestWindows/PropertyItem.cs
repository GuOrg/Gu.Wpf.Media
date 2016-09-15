namespace Gu.Wpf.Media.Demo.UiTestWindows
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    using JetBrains.Annotations;

    public class PropertyItem : INotifyPropertyChanged
    {
        private static readonly Dictionary<DependencyProperty, DependencyProperty> ProxyMap = new Dictionary<DependencyProperty, DependencyProperty>();
        private static readonly Dictionary<DependencyProperty, List<Action>> InvocationList = new Dictionary<DependencyProperty, List<Action>>();
        private readonly MediaElementWrapper wrapper;
        private readonly DependencyProperty proxyProperty;

        public PropertyItem(MediaElementWrapper wrapper, DependencyProperty property)
        {
            this.wrapper = wrapper;
            this.Property = property;
            this.proxyProperty = GetProxy(property);
            InvocationList[this.proxyProperty].Add(() => this.OnPropertyChanged(nameof(this.Value)));
            if (property.ReadOnly)
            {
                var binding = new Binding(property.Name) { Source = wrapper, Mode = BindingMode.OneWay };
                BindingOperations.SetBinding(wrapper, this.proxyProperty, binding);
            }
            else
            {
                var binding = new Binding(property.Name) { Source = wrapper, Mode = BindingMode.TwoWay };
                BindingOperations.SetBinding(wrapper, this.proxyProperty, binding);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DependencyProperty Property { get; }

        public object Value
        {
            get
            {
                return this.wrapper.GetValue(this.Property);
            }
            set
            {
                this.wrapper.SetCurrentValue(this.proxyProperty, value);
                this.OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return $"{nameof(this.Property)}: {this.Property}, {nameof(this.Value)}: {this.Value}";
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static void OnProxyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            foreach (var action in InvocationList[e.Property])
            {
                action();
            }
        }

        private static DependencyProperty GetProxy(DependencyProperty property)
        {
            DependencyProperty proxy;
            if (!ProxyMap.TryGetValue(property, out proxy))
            {
                proxy = DependencyProperty.RegisterAttached(
                 property.Name + "Proxy",
                 typeof(object),
                 typeof(PropertyItem),
                 new PropertyMetadata(null, OnProxyPropertyChanged));
                ProxyMap[property] = proxy;
                InvocationList[proxy] = new List<Action>();
            }

            return proxy;
        }
    }
}