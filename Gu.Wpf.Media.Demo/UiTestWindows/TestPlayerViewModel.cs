namespace Gu.Wpf.Media.Demo.UiTestWindows
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class TestPlayerViewModel : INotifyPropertyChanged
    {
        private string? selectedPropertyName;

        private PropertyItem selectedProperty;

        public TestPlayerViewModel(MediaElementWrapper wrapper)
        {
            this.Properties = MediaElementToPropertyListConverter.GetPropertyItems(wrapper);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ReadOnlyObservableCollection<PropertyItem> Properties { get; }

        public string CoffeeClipFileName => Files.CoffeeClip;

        public PropertyItem SelectedProperty
        {
            get => this.selectedProperty;

            private set
            {
                if (Equals(value, this.selectedProperty))
                {
                    return;
                }

                this.selectedProperty = value;
                this.OnPropertyChanged();
            }
        }

        public string? SelectedPropertyName
        {
            get => this.selectedPropertyName;

            set
            {
                if (value == this.selectedPropertyName)
                {
                    return;
                }

                this.selectedPropertyName = value;
                this.SelectedProperty = this.Properties.SingleOrDefault(x => x.Property.Name == value);
                this.OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
