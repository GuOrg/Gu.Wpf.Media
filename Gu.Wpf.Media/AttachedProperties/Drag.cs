namespace Gu.Wpf.Media
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    public static class Drag
    {
        public static readonly DependencyProperty PauseWhileDraggingProperty = DependencyProperty.RegisterAttached(
            "PauseWhileDragging",
            typeof(MediaElementWrapper),
            typeof(Drag),
            new PropertyMetadata(default(MediaElementWrapper)));

        private static readonly DependencyProperty DragStartStateProperty = DependencyProperty.RegisterAttached(
            "DragStartState",
            typeof(MediaState?),
            typeof(Drag),
            new PropertyMetadata(default(MediaState?)));

        static Drag()
        {
            EventManager.RegisterClassHandler(typeof(Slider), Thumb.DragStartedEvent, new DragStartedEventHandler(OnDragStarted));
            EventManager.RegisterClassHandler(typeof(Slider), Thumb.DragCompletedEvent, new DragCompletedEventHandler(OnDragComplated));
        }

        public static void SetPauseWhileDragging(Slider element, MediaElementWrapper value)
        {
            element.SetValue(PauseWhileDraggingProperty, value);
        }

        [AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
        [AttachedPropertyBrowsableForType(typeof(Slider))]
        public static MediaElementWrapper GetPauseWhileDragging(Slider element)
        {
            return (MediaElementWrapper)element.GetValue(PauseWhileDraggingProperty);
        }

        private static void SetDragStartState(this MediaElementWrapper element, MediaState? value)
        {
            element.SetValue(DragStartStateProperty, value);
        }

        [AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
        [AttachedPropertyBrowsableForType(typeof(MediaElementWrapper))]
        private static MediaState? GetDragStartState(this MediaElementWrapper element)
        {
            return (MediaState?)element.GetValue(DragStartStateProperty);
        }

        private static void OnDragStarted(object sender, DragStartedEventArgs e)
        {
            var wrapper = GetPauseWhileDragging((Slider)sender);
            if (wrapper?.HasMedia != true)
            {
                return;
            }

            wrapper.SetDragStartState(wrapper.State);
            wrapper.Pause();
        }

        private static void OnDragComplated(object sender, DragCompletedEventArgs e)
        {
            var wrapper = GetPauseWhileDragging((Slider)sender);
            if (wrapper?.GetDragStartState() == MediaState.Play)
            {
                wrapper.Play();
            }
        }
    }
}
