namespace Gu.Wpf.Media
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public partial class MediaElementWrapper
    {
        /// <summary>
        /// Identifies the <see cref="MediaElementWrapper.Source" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.Source" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty SourceProperty = MediaElement.SourceProperty.AddOwner(
            typeof(MediaElementWrapper),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                OnSourceChanged,
                OnSourceCoerce));

        /// <summary>
        /// Identifies the <see cref="P:MediaElementWrapper.Volume" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.Volume" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty VolumeProperty = MediaElement.VolumeProperty.AddOwner(
            typeof(MediaElementWrapper),
            new FrameworkPropertyMetadata(
                MediaElement.VolumeProperty.DefaultMetadata.DefaultValue,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnVolumeChanged,
                OnVolumeCoerce));

        /// <summary>
        /// Identifies the <see cref="MediaElementWrapper.Balance" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.Balance" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty BalanceProperty = MediaElement.BalanceProperty.AddOwner(
            typeof(MediaElementWrapper),
            new FrameworkPropertyMetadata(
                 MediaElement.BalanceProperty.DefaultMetadata.DefaultValue,
                 FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                 OnBalanceChanged));

        /// <summary>
        /// Identifies the <see cref="MediaElementWrapper.IsMuted" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.IsMuted" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty IsMutedProperty = MediaElement.IsMutedProperty.AddOwner(
            typeof(MediaElementWrapper),
            new FrameworkPropertyMetadata(
                MediaElement.IsMutedProperty.DefaultMetadata.DefaultValue,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnIsMutedChanged,
                OnIsMutedCoerce));

        /// <summary>
        /// Identifies the <see cref="MediaElementWrapper.ScrubbingEnabled" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.ScrubbingEnabled" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ScrubbingEnabledProperty = MediaElement.ScrubbingEnabledProperty.AddOwner(
            typeof(MediaElementWrapper),
            new PropertyMetadata(
                MediaElement.ScrubbingEnabledProperty.DefaultMetadata.DefaultValue,
                OnIsScrubbingEnabledChanged));

        /// <summary>
        /// DependencyProperty for Stretch property.
        /// </summary>
        /// <seealso cref="MediaElement.Stretch" />
        /// This property is cached and grouped (AspectRatioGroup)
        public static readonly DependencyProperty StretchProperty = Viewbox.StretchProperty.AddOwner(
            typeof(MediaElementWrapper),
            new FrameworkPropertyMetadata(
                 System.Windows.Media.Stretch.None,
                 FrameworkPropertyMetadataOptions.AffectsMeasure,
                 OnStretchChanged));

        /// <summary>
        /// DependencyProperty for StretchDirection property.
        /// </summary>
        /// <seealso cref="Viewbox.Stretch" />
        public static readonly DependencyProperty StretchDirectionProperty = Viewbox.StretchDirectionProperty.AddOwner(
            typeof(MediaElementWrapper),
             new FrameworkPropertyMetadata(
                 StretchDirection.Both,
                 FrameworkPropertyMetadataOptions.AffectsMeasure,
                 OnStretchDirectionChanged));

        /// <summary>
        /// Gets or sets a media source on the <see cref="MediaElementWrapper" />.
        /// </summary>
        /// <returns>
        /// The URI that specifies the source of the element. The default is null.
        /// </returns>
        public Uri Source
        {
            get { return (Uri)this.GetValue(SourceProperty); }
            set { this.SetValue(SourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the media's volume.
        /// </summary>
        /// <returns>
        /// The media's volume represented on a linear scale between 0 and 1. The default is 0.5.
        /// </returns>
        public double Volume
        {
            get { return (double)this.GetValue(VolumeProperty); }
            set { this.SetValue(VolumeProperty, value); }
        }

        /// <summary>
        /// Gets or sets a ratio of volume across speakers.
        /// </summary>
        /// <returns>
        /// The ratio of volume across speakers in the range between -1 and 1. The default is 0.
        /// </returns>
        public double Balance
        {
            get { return (double)this.GetValue(BalanceProperty); }
            set { this.SetValue(BalanceProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the audio is muted.
        /// </summary>
        /// <returns>
        /// true if audio is muted; otherwise, false. The default is false.
        /// </returns>
        public bool IsMuted
        {
            get { return (bool)this.GetValue(IsMutedProperty); }
            set { this.SetValue(IsMutedProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the <see cref="MediaElementWrapper" /> will update frames for seek operations while paused.
        /// </summary>
        /// <returns>
        /// true if frames are updated while paused; otherwise, false. The default is false.
        /// </returns>
        public bool ScrubbingEnabled
        {
            get { return (bool)this.GetValue(ScrubbingEnabledProperty); }
            set { this.SetValue(ScrubbingEnabledProperty, value); }
        }

        /// <summary>
        /// Gets/Sets the Stretch on this MediaElement.
        /// The Stretch property determines how large the MediaElement will be drawn.
        /// </summary>
        /// <seealso cref="MediaElement.StretchProperty" />
        public Stretch Stretch
        {
            get { return (Stretch)this.GetValue(StretchProperty); }
            set { this.SetValue(StretchProperty, value); }
        }

        /// <summary>
        /// Gets/Sets the stretch direction of the Viewbox, which determines the restrictions on
        /// scaling that are applied to the content inside the Viewbox.  For instance, this property
        /// can be used to prevent the content from being smaller than its native size or larger than
        /// its native size.
        /// </summary>
        /// <seealso cref="Viewbox.StretchDirectionProperty" />
        public StretchDirection StretchDirection
        {
            get { return (StretchDirection)this.GetValue(StretchDirectionProperty); }
            set { this.SetValue(StretchDirectionProperty, value); }
        }

        private static object OnSourceCoerce(DependencyObject d, object baseValue)
        {
            var uri = baseValue as Uri;
            if (string.IsNullOrWhiteSpace(uri?.OriginalString))
            {
                return null;
            }

            return baseValue;
        }

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var wrapper = (MediaElementWrapper)d;
            wrapper.OnSourceChanged(e.NewValue as Uri);
        }

        private static void OnVolumeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var wrapper = (MediaElementWrapper)d;
            wrapper.mediaElement.SetCurrentValue(MediaElement.VolumeProperty, e.NewValue);
            var newValue = (double)e.NewValue;
            if (newValue <= 0)
            {
                wrapper.SetCurrentValue(IsMutedProperty, true);
            }
            else if (wrapper.IsMuted && newValue >= 0)
            {
                wrapper.SetCurrentValue(IsMutedProperty, false);
            }
        }

        private static void OnBalanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var wrapper = (MediaElementWrapper)d;
            wrapper.mediaElement.SetCurrentValue(MediaElement.BalanceProperty, e.NewValue);
        }

        private static void OnIsMutedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var wrapper = (MediaElementWrapper)d;
            wrapper.mediaElement.SetCurrentValue(MediaElement.IsMutedProperty, e.NewValue);
        }

        private static void OnIsScrubbingEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var wrapper = (MediaElementWrapper)d;
            wrapper.mediaElement.SetCurrentValue(MediaElement.ScrubbingEnabledProperty, e.NewValue);
        }

        private static void OnStretchChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var wrapper = (MediaElementWrapper)d;
            wrapper.mediaElement.SetCurrentValue(MediaElement.StretchProperty, e.NewValue);
        }

        private static void OnStretchDirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var wrapper = (MediaElementWrapper)d;
            wrapper.mediaElement.SetCurrentValue(MediaElement.StretchDirectionProperty, e.NewValue);
        }
    }
}