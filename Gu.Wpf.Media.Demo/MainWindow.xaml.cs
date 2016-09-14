namespace Gu.Wpf.Media.Demo
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    using Microsoft.Win32;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = $"Media files|{this.MediaElement.VideoFormats}|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                this.MediaElement.Source = new Uri(openFileDialog.FileName);
            }
        }

        private void OnToggleFullScreenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.WindowStyle == WindowStyle.SingleBorderWindow)
            {
                this.WindowStyle = WindowStyle.None;
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.WindowState = WindowState.Normal;
            }
        }
    }
}
