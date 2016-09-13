namespace Gu.Wpf.Media
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public partial class MediaElementWrapper
    {
        /// <summary>
        /// Identifies the <see cref="MediaElementWrapper.Source" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.Source" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty SourceProperty = MediaElement.SourceProperty.AddOwner(typeof(MediaElementWrapper));

        /// <summary>
        /// Identifies the <see cref="P:MediaElementWrapper.Volume" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.Volume" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty VolumeProperty = MediaElement.VolumeProperty.AddOwner(typeof(MediaElementWrapper));

        /// <summary>
        /// Identifies the <see cref="MediaElementWrapper.Balance" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.Balance" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty BalanceProperty = MediaElement.BalanceProperty.AddOwner(typeof(MediaElementWrapper));

        /// <summary>
        /// Identifies the <see cref="MediaElementWrapper.IsMuted" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.IsMuted" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty IsMutedProperty = MediaElement.IsMutedProperty.AddOwner(typeof(MediaElementWrapper));

        /// <summary>
        /// Identifies the <see cref="MediaElementWrapper.ScrubbingEnabled" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.ScrubbingEnabled" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ScrubbingEnabledProperty = MediaElement.ScrubbingEnabledProperty.AddOwner(typeof(MediaElementWrapper));

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
    }
}