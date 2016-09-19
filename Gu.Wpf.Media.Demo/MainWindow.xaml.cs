// ReSharper disable SuppressSetMutable
namespace Gu.Wpf.Media.Demo
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using Microsoft.Win32;

    public partial class MainWindow : Window
    {
        private Stretch stretch;

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
                this.MediaElement.SetCurrentValue(MediaElementWrapper.SourceProperty, new Uri(openFileDialog.FileName));
            }
        }

        private void OnToggleFullScreenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.WindowStyle == WindowStyle.SingleBorderWindow)
            {
                this.stretch = this.MediaElement.Stretch;
                this.MediaElement.Stretch = Stretch.Uniform;
                this.WindowStyle = WindowStyle.None;
                this.SizeToContent = SizeToContent.Manual;
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.MediaElement.Stretch = this.stretch;
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.SizeToContent = SizeToContent.WidthAndHeight;
                this.WindowState = WindowState.Normal;
            }

            e.Handled = true;
        }

        private void OnEndFullScreenCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.WindowState == WindowState.Maximized && this.WindowStyle == WindowStyle.None;
        }

        private void OnEndFullScreenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.MediaElement.Stretch = this.stretch;
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.WindowState = WindowState.Normal;
            e.Handled = true;
        }

        private void OnCloseExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
            e.Handled = true;
        }

        private void OnMinimizeCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.WindowState != WindowState.Minimized;
            e.Handled = true;
        }

        private void OnMinimizeExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void OnMaximizeCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.WindowState != WindowState.Maximized;
            e.Handled = true;
        }

        private void OnMaximizeExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.SizeToContent = SizeToContent.Manual;
            this.WindowState = WindowState.Maximized;
        }

        private void OnRestoreCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.WindowState != WindowState.Normal;
            e.Handled = true;
        }

        private void OnRestoreExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.WindowState = WindowState.Normal;
        }
    }
}
