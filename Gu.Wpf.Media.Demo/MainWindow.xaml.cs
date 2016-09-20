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

        private static readonly DependencyPropertyKey MediaFileNamePropertyKey = DependencyProperty.RegisterReadOnly(
            "MediaFileName",
            typeof(string),
            typeof(MainWindow),
            new PropertyMetadata(default(string)));

        public static readonly DependencyProperty MediaFileNameProperty = MediaFileNamePropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey MediaUriPropertyKey = DependencyProperty.RegisterReadOnly(
            "MediaUri",
            typeof(Uri),
            typeof(MainWindow),
            new PropertyMetadata(default(Uri)));

        public static readonly DependencyProperty MediaUriProperty = MediaUriPropertyKey.DependencyProperty;

        public MainWindow()
        {
            this.InitializeComponent();
        }

        public Uri MediaUri
        {
            get { return (Uri)this.GetValue(MediaUriProperty); }
            protected set { this.SetValue(MediaUriPropertyKey, value); }
        }

        public string MediaFileName
        {
            get { return (string)this.GetValue(MediaFileNameProperty); }
            protected set { this.SetValue(MediaFileNamePropertyKey, value); }
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files == null)
                {
                    return;
                }

                if (files.Length > 1)
                {
                    MessageBox.Show(
                        this,
                        "Only onde file at the time can be dropped.",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);

                    return;
                }

                try
                {
                    this.MediaFileName = files[0];
                    this.MediaUri = new Uri(this.MediaFileName);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(
                        this,
                        exception.Message,
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }


        private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = $"Media files|{this.MediaElement.VideoFormats}|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                this.MediaFileName = openFileDialog.FileName;
                this.MediaUri = new Uri(this.MediaFileName);
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

        private void OnMediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(this, e.ErrorException.Message, "Media failed");
        }

        ////private void OnCloseExecuted(object sender, ExecutedRoutedEventArgs e)
        ////{
        ////    this.Close();
        ////    e.Handled = true;
        ////}

        ////private void OnMinimizeCanExecute(object sender, CanExecuteRoutedEventArgs e)
        ////{
        ////    e.CanExecute = this.WindowState != WindowState.Minimized;
        ////    e.Handled = true;
        ////}

        ////private void OnMinimizeExecuted(object sender, ExecutedRoutedEventArgs e)
        ////{
        ////    this.WindowState = WindowState.Minimized;
        ////}

        ////private void OnMaximizeCanExecute(object sender, CanExecuteRoutedEventArgs e)
        ////{
        ////    e.CanExecute = this.WindowState != WindowState.Maximized;
        ////    e.Handled = true;
        ////}

        ////private void OnMaximizeExecuted(object sender, ExecutedRoutedEventArgs e)
        ////{
        ////    this.SizeToContent = SizeToContent.Manual;
        ////    this.WindowState = WindowState.Maximized;
        ////}

        ////private void OnRestoreCanExecute(object sender, CanExecuteRoutedEventArgs e)
        ////{
        ////    e.CanExecute = this.WindowState != WindowState.Normal;
        ////    e.Handled = true;
        ////}

        ////private void OnRestoreExecuted(object sender, ExecutedRoutedEventArgs e)
        ////{
        ////    this.SizeToContent = SizeToContent.WidthAndHeight;
        ////    this.WindowState = WindowState.Normal;
        ////}
    }
}
