namespace Gu.Wpf.Media.UiTests
{
    using System;
    using System.Threading;
    using System.Windows;

    using Gu.Wpf.Media.UiTests.Helpers;

    using NUnit.Framework;

    using TestStack.White.UIItems;

    public partial class TestPlayerWindowTests
    {
        [Test]
        public void Loads()
        {
            this.AreEqual("0", false, MediaElementWrapper.BalanceProperty);
            this.AreEqual(FileFormats.DefaultAudioFormats, false, MediaElementWrapper.AudioFormatsProperty);
            this.AreEqual("0", true, MediaElementWrapper.BufferingProgressProperty);
            this.AreEqual("null", true, MediaElementWrapper.CanPauseMediaProperty);
            this.AreEqual("0", true, MediaElementWrapper.DownloadProgressProperty);
            this.AreEqual("null", true, MediaElementWrapper.HasAudioProperty);
            this.AreEqual("False", true, MediaElementWrapper.HasMediaProperty);
            this.AreEqual("null", true, MediaElementWrapper.HasVideoProperty);
            this.AreEqual("False", true, MediaElementWrapper.IsBufferingProperty);
            this.AreEqual("False", false, MediaElementWrapper.IsMutedProperty);
            this.AreEqual("False", false, MediaElementWrapper.IsPlayingProperty);
            this.AreEqual("null", true, MediaElementWrapper.LengthProperty);
            this.AreEqual("null", true, MediaElementWrapper.NaturalVideoHeightProperty);
            this.AreEqual("null", true, MediaElementWrapper.NaturalVideoWidthProperty);
            this.AreEqual("null", false, MediaElementWrapper.PositionProperty);
            this.AreEqual("True", false, MediaElementWrapper.ScrubbingEnabledProperty);
            this.AreEqual("", false, MediaElementWrapper.SourceProperty);
            this.AreEqual("0", false, MediaElementWrapper.SpeedRatioProperty);
            this.AreEqual("Manual", false, MediaElementWrapper.StateProperty);
            this.AreEqual("None", false, MediaElementWrapper.StretchProperty);
            this.AreEqual("Both", false, MediaElementWrapper.StretchDirectionProperty);
            this.AreEqual(FileFormats.DefaultVideoFormats, false, MediaElementWrapper.VideoFormatsProperty);
            this.AreEqual("0.5", false, MediaElementWrapper.VolumeProperty);
            this.AreEqual("0.05", false, MediaElementWrapper.VolumeIncrementProperty);
        }

        [Test]
        public void SetSourceToCoffeClip()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.AreEqual("0", false, MediaElementWrapper.BalanceProperty);
            this.AreEqual("0", true, MediaElementWrapper.BufferingProgressProperty);
            this.AreEqual("True", true, MediaElementWrapper.CanPauseMediaProperty);
            this.AreEqual("0", true, MediaElementWrapper.DownloadProgressProperty);
            this.AreEqual("True", true, MediaElementWrapper.HasAudioProperty);
            this.AreEqual("True", true, MediaElementWrapper.HasMediaProperty);
            this.AreEqual("True", true, MediaElementWrapper.HasVideoProperty);
            this.AreEqual("False", true, MediaElementWrapper.IsBufferingProperty);
            this.AreEqual("False", false, MediaElementWrapper.IsMutedProperty);
            this.AreEqual("False", false, MediaElementWrapper.IsPlayingProperty);
            this.AreEqual("00:00:11", true, MediaElementWrapper.LengthProperty);
            this.AreEqual("540", true, MediaElementWrapper.NaturalVideoHeightProperty);
            this.AreEqual("960", true, MediaElementWrapper.NaturalVideoWidthProperty);
            this.AreEqual("00:00:00", false, MediaElementWrapper.PositionProperty);
            this.AreEqual("True", false, MediaElementWrapper.ScrubbingEnabledProperty);
            this.AreEqual(Info.CoffeeClipFileName(), false, MediaElementWrapper.SourceProperty);
            this.AreEqual("0", false, MediaElementWrapper.SpeedRatioProperty);
            this.AreEqual("Pause", false, MediaElementWrapper.StateProperty);
            this.AreEqual("None", false, MediaElementWrapper.StretchProperty);
            this.AreEqual("Both", false, MediaElementWrapper.StretchDirectionProperty);
            this.AreEqual("0.5", false, MediaElementWrapper.VolumeProperty);
            this.AreEqual("0.05", false, MediaElementWrapper.VolumeIncrementProperty);
        }

        [Test]
        public void SetIsPlayingTrueThenFalse()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            var position = this.GetValue(MediaElementWrapper.PositionProperty);
            this.SetValue(MediaElementWrapper.IsPlayingProperty, "True");
            this.AreEqual("True", false, MediaElementWrapper.IsPlayingProperty);
            this.AreEqual("Play", false, MediaElementWrapper.StateProperty);
            Thread.Sleep(TimeSpan.FromSeconds(0.2));
            this.SetValue(MediaElementWrapper.IsPlayingProperty, "False");
            this.AreEqual("False", false, MediaElementWrapper.IsPlayingProperty);
            this.AreEqual("Pause", false, MediaElementWrapper.StateProperty);
            Assert.AreNotEqual(position, this.GetValue(MediaElementWrapper.PositionProperty));
        }

        [TestCase("Pause")]
        [TestCase("Stop")]
        public void SetStatePlayThen(string nextState)
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            var position = this.GetValue(MediaElementWrapper.PositionProperty);
            this.SetValue(MediaElementWrapper.StateProperty, "Play");
            this.AreEqual("True", false, MediaElementWrapper.IsPlayingProperty);
            this.AreEqual("Play", false, MediaElementWrapper.StateProperty);
            Thread.Sleep(TimeSpan.FromSeconds(0.2));
            this.SetValue(MediaElementWrapper.StateProperty, nextState);
            this.AreEqual("False", false, MediaElementWrapper.IsPlayingProperty);
            this.AreEqual(nextState, false, MediaElementWrapper.StateProperty);
            Assert.AreNotEqual(position, this.GetValue(MediaElementWrapper.PositionProperty));
        }
    }
}