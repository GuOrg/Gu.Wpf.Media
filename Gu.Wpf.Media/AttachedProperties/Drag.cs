﻿namespace Gu.Wpf.Media
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    /// <summary>
    /// Exposes attached properties that can be useful when controlling a player with a slider.
    /// </summary>
    public static class Drag
    {
        /// <summary>
        /// This is an attached property.
        /// Setting this to {Binding ElementName=SomeMediaElementWrapper} will pause playback when the slider controlling postion is dragged.
        /// </summary>
        public static readonly DependencyProperty PauseWhileDraggingProperty = DependencyProperty.RegisterAttached(
            "PauseWhileDragging",
            typeof(MediaElementWrapper),
            typeof(Drag),
            new PropertyMetadata(default(MediaElementWrapper)));

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

        private static void OnDragStarted(object sender, DragStartedEventArgs e)
        {
            var wrapper = GetPauseWhileDragging((Slider)sender);
            if (wrapper?.HasMedia != true)
            {
                return;
            }

            wrapper.Break();
        }

        private static void OnDragComplated(object sender, DragCompletedEventArgs e)
        {
            var wrapper = GetPauseWhileDragging((Slider)sender);
            if (wrapper.IsPlaying)
            {
                wrapper.Resume();
            }
        }
    }
}