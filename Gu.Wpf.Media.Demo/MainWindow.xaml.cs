namespace Gu.Wpf.Media.Demo
{
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            var builder = new StringBuilder();
            int count = 0;
            foreach (var propertyInfo in typeof(MediaElementWrapper).GetEvents().Where(p => typeof(Decorator).GetEvents()
                                                                                                        .All(wp => wp.Name != p.Name)))
            {
                count++;
                builder.AppendLine($"## 2.{count}. {propertyInfo.Name}");
            }

            var props = builder.ToString();
        }
    }
}
