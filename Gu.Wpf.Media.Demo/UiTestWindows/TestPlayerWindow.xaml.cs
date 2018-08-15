namespace Gu.Wpf.Media.Demo.UiTestWindows
{
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Controls;
    using System.Windows.Data;

    public partial class TestPlayerWindow : Window
    {
        public TestPlayerWindow()
        {
            this.InitializeComponent();
            var dps = typeof(MediaElementWrapper).GetFields(BindingFlags.Static | BindingFlags.Public)
                                     .Where(f => f.FieldType == typeof(DependencyProperty))
                                     .Select(f => f.GetValue(null))
                                     .OfType<DependencyProperty>()
                                     .OrderBy(x => x.Name)
                                     .ToArray();
            int row = 0;
            foreach (var dp in dps)
            {
                this.PropertyGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                var label = new TextBox { Text = dp.Name, IsReadOnly = true, IsReadOnlyCaretVisible = true };
                Grid.SetRow(label, row);
                Grid.SetColumn(label, 0);
                this.PropertyGrid.Children.Add(label);

                var value = new TextBox();
                Grid.SetRow(value, row);
                Grid.SetColumn(value, 1);
                AutomationProperties.SetAutomationId(value, dp.Name);
                if (dp.ReadOnly)
                {
                    var binding = new Binding
                    {
                        Path = new PropertyPath(dp.Name),
                        ElementName = nameof(this.MediaElement),
                        Mode = BindingMode.OneWay,
                        Converter = NullConverter.Default
                    };
                    _ = BindingOperations.SetBinding(value, TextBox.TextProperty, binding);
                    value.IsEnabled = false;
                }
                else
                {
                    var binding = new Binding
                    {
                        Path = new PropertyPath(dp.Name),
                        ElementName = nameof(this.MediaElement),
                        Mode = BindingMode.TwoWay,
                        Converter = NullConverter.Default
                    };
                    _ = BindingOperations.SetBinding(value, TextBox.TextProperty, binding);
                    value.IsEnabled = true;
                }

                this.PropertyGrid.Children.Add(value);
                row++;
            }

            this.PropertyGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        }
    }
}
