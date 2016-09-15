namespace Gu.Wpf.Media.Demo.UiTestWindows
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    using JetBrains.Annotations;

    public class PropertyItem : INotifyPropertyChanged
    {
        private object value;

        public PropertyItem(MediaElementWrapper wrapper, DependencyProperty property)
        {
            this.Property = property;
            DependencyProperty.RegisterAttached(
                property.Name + "Proxy",
                property.PropertyType,
                typeof(PropertyItem),
                new PropertyMetadata(property.DefaultMetadata.DefaultValue, this.OnPropertyChanged));


        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DependencyProperty Property { get; }

        public object Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (Equals(value, this.value)) return;
                this.value = value;
                this.OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}