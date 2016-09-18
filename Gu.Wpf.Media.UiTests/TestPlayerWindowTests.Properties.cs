namespace Gu.Wpf.Media.UiTests
{
    using System;
    using System.IO;
    using System.Threading;
    using Gu.Wpf.Media.UiTests.Helpers;

    using NUnit.Framework;

    public partial class TestPlayerWindowTests
    {
        [Test]
        public void DefaultValues()
        {
            this.RestartApplication();
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
            this.AreEqual("False", false, MediaElementWrapper.ScrubbingEnabledProperty);
            this.AreEqual("null", false, MediaElementWrapper.SourceProperty);
            this.AreEqual("0", false, MediaElementWrapper.SpeedRatioProperty);
            this.AreEqual("Play", false, MediaElementWrapper.LoadedBehaviorProperty);
            this.AreEqual("Stop", false, MediaElementWrapper.StateProperty);
            this.AreEqual("None", false, MediaElementWrapper.StretchProperty);
            this.AreEqual("Both", false, MediaElementWrapper.StretchDirectionProperty);
            this.AreEqual(FileFormats.DefaultVideoFormats, false, MediaElementWrapper.VideoFormatsProperty);
            this.AreEqual("0.5", false, MediaElementWrapper.VolumeProperty);
            this.AreEqual("0.05", false, MediaElementWrapper.VolumeIncrementProperty);
        }

        [Test]
        public void SetSourceToCoffeeClipLoadedBehaviorPause()
        {
            this.SetValue(MediaElementWrapper.LoadedBehaviorProperty, "Pause");
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
            this.AreEqual("False", false, MediaElementWrapper.ScrubbingEnabledProperty);
            this.AreEqual(Info.CoffeeClipFileName(), false, MediaElementWrapper.SourceProperty);
            this.AreEqual("0", false, MediaElementWrapper.SpeedRatioProperty);
            this.AreEqual("Pause", false, MediaElementWrapper.StateProperty);
            this.AreEqual("None", false, MediaElementWrapper.StretchProperty);
            this.AreEqual("Both", false, MediaElementWrapper.StretchDirectionProperty);
            this.AreEqual("0.5", false, MediaElementWrapper.VolumeProperty);
            this.AreEqual("0.05", false, MediaElementWrapper.VolumeIncrementProperty);
        }

        [Test]
        public void SetSourceToCoffeeClipThenToOther()
        {
            this.SetValue(MediaElementWrapper.LoadedBehaviorProperty, "Pause");
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.PlayButton.Click();
            var otherFile = Info.CoffeeClipFileName().Replace("coffee.mp4", "other.mp4");
            if (!File.Exists(otherFile))
            {
                File.Copy(Info.CoffeeClipFileName(), otherFile);
            }

            this.SetValue(MediaElementWrapper.SourceProperty, otherFile);
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
            this.AreEqual("False", false, MediaElementWrapper.ScrubbingEnabledProperty);
            this.AreEqual(otherFile, false, MediaElementWrapper.SourceProperty);
            this.AreEqual("0", false, MediaElementWrapper.SpeedRatioProperty);
            this.AreEqual("Pause", false, MediaElementWrapper.StateProperty);
            this.AreEqual("None", false, MediaElementWrapper.StretchProperty);
            this.AreEqual("Both", false, MediaElementWrapper.StretchDirectionProperty);
            this.AreEqual("0.5", false, MediaElementWrapper.VolumeProperty);
            this.AreEqual("0.05", false, MediaElementWrapper.VolumeIncrementProperty);
        }

        [Test]
        public void SetSourceToError()
        {
            this.SetValue(MediaElementWrapper.LoadedBehaviorProperty, "Play");
            var missingFile = Path.ChangeExtension(Info.CoffeeClipFileName(), ".error");
            this.SetValue(MediaElementWrapper.SourceProperty, missingFile);
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
            this.AreEqual("False", false, MediaElementWrapper.ScrubbingEnabledProperty);
            this.AreEqual(missingFile, false, MediaElementWrapper.SourceProperty);
            this.AreEqual("0", false, MediaElementWrapper.SpeedRatioProperty);
            this.AreEqual("Play", false, MediaElementWrapper.LoadedBehaviorProperty);
            this.AreEqual("Stop", false, MediaElementWrapper.StateProperty);
            this.AreEqual("None", false, MediaElementWrapper.StretchProperty);
            this.AreEqual("Both", false, MediaElementWrapper.StretchDirectionProperty);
            this.AreEqual(FileFormats.DefaultVideoFormats, false, MediaElementWrapper.VideoFormatsProperty);
            this.AreEqual("0.5", false, MediaElementWrapper.VolumeProperty);
            this.AreEqual("0.05", false, MediaElementWrapper.VolumeIncrementProperty);
        }

        [Test]
        public void SetSourceToCoffeeClipLoadedBehaviorPlay()
        {
            this.SetValue(MediaElementWrapper.LoadedBehaviorProperty, "Play");
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.AreEqual("True", false, MediaElementWrapper.IsPlayingProperty);
            this.AreEqual(Info.CoffeeClipFileName(), false, MediaElementWrapper.SourceProperty);
            this.AreEqual("Play", false, MediaElementWrapper.StateProperty);
        }

        [Test]
        public void SetSourceToCoffeeClipLoadedBehaviorStop()
        {
            this.SetValue(MediaElementWrapper.LoadedBehaviorProperty, "Stop");
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.AreEqual("False", false, MediaElementWrapper.IsPlayingProperty);
            this.AreEqual(Info.CoffeeClipFileName(), false, MediaElementWrapper.SourceProperty);
            this.AreEqual("Stop", false, MediaElementWrapper.StateProperty);
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