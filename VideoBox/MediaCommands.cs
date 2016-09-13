//namespace VideoBox
//{
//    using System.Windows;
//    using System.Windows.Controls;
//    using System.Windows.Input;
//    using System.Windows.Media;

//    public class MediaCommands
//    {
//        private static readonly DependencyPropertyKey PlayCommandPropertyKey = DependencyProperty.RegisterAttachedReadOnly(
//            "PlayCommand",
//            typeof(ICommand),
//            typeof(MediaCommands),
//            new PropertyMetadata(default(ICommand)));

//        public static readonly DependencyProperty PlayCommandProperty = PlayCommandPropertyKey.DependencyProperty;

//        static MediaCommands()
//        {
//            EventManager.RegisterClassHandler(typeof(MediaElement), FrameworkElement.LoadedEvent, new RoutedEventHandler(OnLoaded));
//        }

//        public static void SetPlayCommand(MediaElement element, ICommand value)
//        {
//            element.SetValue(PlayCommandPropertyKey, value);
//        }

//        public static ICommand GetPlayCommand(MediaElement element)
//        {
//            return (ICommand)element.GetValue(PlayCommandProperty);
//        }

//        private static void OnLoaded(object sender, RoutedEventArgs e)
//        {
//            var mediaElement = (MediaElement)sender;
//            var commandBinding = new CommandBinding(System.Windows.Input.MediaCommands.Play);
//            commandBinding.Executed += (o, __) => ((MediaElement)o).Play();
//            commandBinding.CanExecute += (o, args) => args.CanExecute = ((MediaElement)o).HasVideo;
//            mediaElement.CommandBindings.Add(commandBinding);
//            SetPlayCommand(mediaElement, commandBinding.Command);
//        }
//    }
//}
