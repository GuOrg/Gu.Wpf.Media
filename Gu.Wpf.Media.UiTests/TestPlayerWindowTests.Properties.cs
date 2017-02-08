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
            this.AreEqual("0", MediaElementWrapper.BalanceProperty);
            this.AreEqual(FileFormats.DefaultAudioFormats, MediaElementWrapper.AudioFormatsProperty);
            this.AreEqual("0", MediaElementWrapper.BufferingProgressProperty);
            this.AreEqual("null", MediaElementWrapper.CanPauseProperty);
            this.AreEqual("0", MediaElementWrapper.DownloadProgressProperty);
            this.AreEqual("null", MediaElementWrapper.HasAudioProperty);
            this.AreEqual("False", MediaElementWrapper.HasMediaProperty);
            this.AreEqual("null", MediaElementWrapper.HasVideoProperty);
            this.AreEqual("False", MediaElementWrapper.IsBufferingProperty);
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
            this.AreEqual("False", MediaElementWrapper.IsPlayingProperty);
            this.AreEqual("null", MediaElementWrapper.LengthProperty);
            this.AreEqual("null", MediaElementWrapper.NaturalVideoHeightProperty);
            this.AreEqual("null", MediaElementWrapper.NaturalVideoWidthProperty);
            this.AreEqual("null", MediaElementWrapper.PositionProperty);
            this.AreEqual("False", MediaElementWrapper.ScrubbingEnabledProperty);
            this.AreEqual("null", MediaElementWrapper.SourceProperty);
            this.AreEqual("0", MediaElementWrapper.SpeedRatioProperty);
            this.AreEqual("Play", MediaElementWrapper.LoadedBehaviorProperty);
            this.AreEqual("Stop", MediaElementWrapper.StateProperty);
            this.AreEqual("None", MediaElementWrapper.StretchProperty);
            this.AreEqual("Both", MediaElementWrapper.StretchDirectionProperty);
            this.AreEqual(FileFormats.DefaultVideoFormats, MediaElementWrapper.VideoFormatsProperty);
            this.AreEqual("0.5", MediaElementWrapper.VolumeProperty);
            this.AreEqual("0.05", MediaElementWrapper.VolumeIncrementProperty);
        }

        [Test]
        public void Readonly()
        {
            this.RestartApplication();
            this.AssertReadOnly(false, MediaElementWrapper.BalanceProperty);
            this.AssertReadOnly(false, MediaElementWrapper.AudioFormatsProperty);
            this.AssertReadOnly(true, MediaElementWrapper.BufferingProgressProperty);
            this.AssertReadOnly(true, MediaElementWrapper.CanPauseProperty);
            this.AssertReadOnly(true, MediaElementWrapper.DownloadProgressProperty);
            this.AssertReadOnly(true, MediaElementWrapper.HasAudioProperty);
            this.AssertReadOnly(true, MediaElementWrapper.HasMediaProperty);
            this.AssertReadOnly(true, MediaElementWrapper.HasVideoProperty);
            this.AssertReadOnly(true, MediaElementWrapper.IsBufferingProperty);
            this.AssertReadOnly(false, MediaElementWrapper.IsMutedProperty);
            this.AssertReadOnly(false, MediaElementWrapper.IsPlayingProperty);
            this.AssertReadOnly(true, MediaElementWrapper.LengthProperty);
            this.AssertReadOnly(true, MediaElementWrapper.NaturalVideoHeightProperty);
            this.AssertReadOnly(true, MediaElementWrapper.NaturalVideoWidthProperty);
            this.AssertReadOnly(false, MediaElementWrapper.PositionProperty);
            this.AssertReadOnly(false, MediaElementWrapper.ScrubbingEnabledProperty);
            this.AssertReadOnly(false, MediaElementWrapper.SourceProperty);
            this.AssertReadOnly(false, MediaElementWrapper.SpeedRatioProperty);
            this.AssertReadOnly(false, MediaElementWrapper.LoadedBehaviorProperty);
            this.AssertReadOnly(false, MediaElementWrapper.StateProperty);
            this.AssertReadOnly(false, MediaElementWrapper.StretchProperty);
            this.AssertReadOnly(false, MediaElementWrapper.StretchDirectionProperty);
            this.AssertReadOnly(false, MediaElementWrapper.VideoFormatsProperty);
            this.AssertReadOnly(false, MediaElementWrapper.VolumeProperty);
            this.AssertReadOnly(false, MediaElementWrapper.VolumeIncrementProperty);
        }

        [Test]
        public void SetSourceToCoffeeClipLoadedBehaviorPause()
        {
            this.SetValue(MediaElementWrapper.LoadedBehaviorProperty, "Pause");
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.AreEqual("0", MediaElementWrapper.BalanceProperty);
            this.AreEqual("0", MediaElementWrapper.BufferingProgressProperty);
            this.AreEqual("True", MediaElementWrapper.CanPauseProperty);
            this.AreEqual("0", MediaElementWrapper.DownloadProgressProperty);
            this.AreEqual("True", MediaElementWrapper.HasAudioProperty);
            this.AreEqual("True", MediaElementWrapper.HasMediaProperty);
            this.AreEqual("True", MediaElementWrapper.HasVideoProperty);
            this.AreEqual("False", MediaElementWrapper.IsBufferingProperty);
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
            this.AreEqual("False", MediaElementWrapper.IsPlayingProperty);
            this.AreEqual("00:00:11", MediaElementWrapper.LengthProperty);
            this.AreEqual("540", MediaElementWrapper.NaturalVideoHeightProperty);
            this.AreEqual("960", MediaElementWrapper.NaturalVideoWidthProperty);
            this.AreEqual("00:00:00", MediaElementWrapper.PositionProperty);
            this.AreEqual("False", MediaElementWrapper.ScrubbingEnabledProperty);
            this.AreEqual(Info.CoffeeClipFileName(), MediaElementWrapper.SourceProperty);
            this.AreEqual("0", MediaElementWrapper.SpeedRatioProperty);
            this.AreEqual("Pause", MediaElementWrapper.StateProperty);
            this.AreEqual("None", MediaElementWrapper.StretchProperty);
            this.AreEqual("Both", MediaElementWrapper.StretchDirectionProperty);
            this.AreEqual("0.5", MediaElementWrapper.VolumeProperty);
            this.AreEqual("0.05", MediaElementWrapper.VolumeIncrementProperty);
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
            this.AreEqual("0", MediaElementWrapper.BalanceProperty);
            this.AreEqual("0", MediaElementWrapper.BufferingProgressProperty);
            this.AreEqual("True", MediaElementWrapper.CanPauseProperty);
            this.AreEqual("0", MediaElementWrapper.DownloadProgressProperty);
            this.AreEqual("True", MediaElementWrapper.HasAudioProperty);
            this.AreEqual("True", MediaElementWrapper.HasMediaProperty);
            this.AreEqual("True", MediaElementWrapper.HasVideoProperty);
            this.AreEqual("False", MediaElementWrapper.IsBufferingProperty);
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
            this.AreEqual("False", MediaElementWrapper.IsPlayingProperty);
            this.AreEqual("00:00:11", MediaElementWrapper.LengthProperty);
            this.AreEqual("540", MediaElementWrapper.NaturalVideoHeightProperty);
            this.AreEqual("960", MediaElementWrapper.NaturalVideoWidthProperty);
            this.AreEqual("00:00:00", MediaElementWrapper.PositionProperty);
            this.AreEqual("False", MediaElementWrapper.ScrubbingEnabledProperty);
            this.AreEqual(otherFile, MediaElementWrapper.SourceProperty);
            this.AreEqual("0", MediaElementWrapper.SpeedRatioProperty);
            this.AreEqual("Pause", MediaElementWrapper.StateProperty);
            this.AreEqual("None", MediaElementWrapper.StretchProperty);
            this.AreEqual("Both", MediaElementWrapper.StretchDirectionProperty);
            this.AreEqual("0.5", MediaElementWrapper.VolumeProperty);
            this.AreEqual("0.05", MediaElementWrapper.VolumeIncrementProperty);
        }

        [Test]
        public void SetSourceToClipWithHashInPath()
        {
            var otherFile = Info.CoffeeClipFileName().Replace(@"Samples\coffee.mp4", @"# Samples #\coffee.mp4");
            var directoryName = Path.GetDirectoryName(otherFile);
            if (!Directory.Exists(directoryName))
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                Directory.CreateDirectory(directoryName);
            }

            if (!File.Exists(otherFile))
            {
                File.Copy(Info.CoffeeClipFileName(), otherFile);
            }

            this.SetValue(MediaElementWrapper.SourceProperty, otherFile);
            this.AreEqual("0", MediaElementWrapper.BalanceProperty);
            this.AreEqual("0", MediaElementWrapper.BufferingProgressProperty);
            this.AreEqual("True", MediaElementWrapper.CanPauseProperty);
            this.AreEqual("0", MediaElementWrapper.DownloadProgressProperty);
            this.AreEqual("True", MediaElementWrapper.HasAudioProperty);
            this.AreEqual("True", MediaElementWrapper.HasMediaProperty);
            this.AreEqual("True", MediaElementWrapper.HasVideoProperty);
            this.AreEqual("False", MediaElementWrapper.IsBufferingProperty);
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
            this.AreEqual("False", MediaElementWrapper.IsPlayingProperty);
            this.AreEqual("00:00:11", MediaElementWrapper.LengthProperty);
            this.AreEqual("540", MediaElementWrapper.NaturalVideoHeightProperty);
            this.AreEqual("960", MediaElementWrapper.NaturalVideoWidthProperty);
            this.AreEqual("00:00:00", MediaElementWrapper.PositionProperty);
            this.AreEqual("False", MediaElementWrapper.ScrubbingEnabledProperty);
            this.AreEqual(otherFile, MediaElementWrapper.SourceProperty);
            this.AreEqual("0", MediaElementWrapper.SpeedRatioProperty);
            this.AreEqual("Pause", MediaElementWrapper.StateProperty);
            this.AreEqual("None", MediaElementWrapper.StretchProperty);
            this.AreEqual("Both", MediaElementWrapper.StretchDirectionProperty);
            this.AreEqual("0.5", MediaElementWrapper.VolumeProperty);
            this.AreEqual("0.05", MediaElementWrapper.VolumeIncrementProperty);
        }

        [Test]
        public void SetSourceToError()
        {
            this.SetValue(MediaElementWrapper.LoadedBehaviorProperty, "Play");
            var missingFile = Path.ChangeExtension(Info.CoffeeClipFileName(), ".error");
            this.SetValue(MediaElementWrapper.SourceProperty, missingFile);
            this.AreEqual(FileFormats.DefaultAudioFormats, MediaElementWrapper.AudioFormatsProperty);
            this.AreEqual("0", MediaElementWrapper.BufferingProgressProperty);
            this.AreEqual("null", MediaElementWrapper.CanPauseProperty);
            this.AreEqual("0", MediaElementWrapper.DownloadProgressProperty);
            this.AreEqual("null", MediaElementWrapper.HasAudioProperty);
            this.AreEqual("False", MediaElementWrapper.HasMediaProperty);
            this.AreEqual("null", MediaElementWrapper.HasVideoProperty);
            this.AreEqual("False", MediaElementWrapper.IsBufferingProperty);
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
            this.AreEqual("False", MediaElementWrapper.IsPlayingProperty);
            this.AreEqual("null", MediaElementWrapper.LengthProperty);
            this.AreEqual("null", MediaElementWrapper.NaturalVideoHeightProperty);
            this.AreEqual("null", MediaElementWrapper.NaturalVideoWidthProperty);
            this.AreEqual("null", MediaElementWrapper.PositionProperty);
            this.AreEqual("False", MediaElementWrapper.ScrubbingEnabledProperty);
            this.AreEqual(missingFile, MediaElementWrapper.SourceProperty);
            this.AreEqual("0", MediaElementWrapper.SpeedRatioProperty);
            this.AreEqual("Play", MediaElementWrapper.LoadedBehaviorProperty);
            this.AreEqual("Stop", MediaElementWrapper.StateProperty);
            this.AreEqual("None", MediaElementWrapper.StretchProperty);
            this.AreEqual("Both", MediaElementWrapper.StretchDirectionProperty);
            this.AreEqual(FileFormats.DefaultVideoFormats, MediaElementWrapper.VideoFormatsProperty);
            this.AreEqual("0.5", MediaElementWrapper.VolumeProperty);
            this.AreEqual("0.05", MediaElementWrapper.VolumeIncrementProperty);
        }

        [Test]
        public void SetSourceToCoffeeClipLoadedBehaviorPlay()
        {
            this.SetValue(MediaElementWrapper.LoadedBehaviorProperty, "Play");
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.AreEqual("True", MediaElementWrapper.IsPlayingProperty);
            this.AreEqual(Info.CoffeeClipFileName(), MediaElementWrapper.SourceProperty);
            this.AreEqual("Play", MediaElementWrapper.StateProperty);
        }

        [Test]
        public void SetSourceToCoffeeClipLoadedBehaviorStop()
        {
            this.SetValue(MediaElementWrapper.LoadedBehaviorProperty, "Stop");
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.AreEqual("False", MediaElementWrapper.IsPlayingProperty);
            this.AreEqual(Info.CoffeeClipFileName(), MediaElementWrapper.SourceProperty);
            this.AreEqual("Stop", MediaElementWrapper.StateProperty);
        }

        [Test]
        public void SetIsPlayingTrueThenFalse()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            var position = this.GetValue(MediaElementWrapper.PositionProperty);
            this.SetValue(MediaElementWrapper.IsPlayingProperty, "True");
            this.AreEqual("True", MediaElementWrapper.IsPlayingProperty);
            this.AreEqual("Play", MediaElementWrapper.StateProperty);
            Thread.Sleep(TimeSpan.FromSeconds(0.2));
            this.SetValue(MediaElementWrapper.IsPlayingProperty, "False");
            this.AreEqual("False", MediaElementWrapper.IsPlayingProperty);
            this.AreEqual("Pause", MediaElementWrapper.StateProperty);
            Assert.AreNotEqual(position, this.GetValue(MediaElementWrapper.PositionProperty));
        }

        [TestCase("Pause")]
        [TestCase("Stop")]
        public void SetStatePlayThen(string nextState)
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            var position = this.GetValue(MediaElementWrapper.PositionProperty);
            this.SetValue(MediaElementWrapper.StateProperty, "Play");
            this.AreEqual("True", MediaElementWrapper.IsPlayingProperty);
            this.AreEqual("Play", MediaElementWrapper.StateProperty);
            Thread.Sleep(TimeSpan.FromSeconds(0.2));
            this.SetValue(MediaElementWrapper.StateProperty, nextState);
            this.Window.WaitWhileBusy();
            this.AreEqual("False", MediaElementWrapper.IsPlayingProperty);
            this.AreEqual(nextState, MediaElementWrapper.StateProperty);
            Assert.AreNotEqual(position, this.GetValue(MediaElementWrapper.PositionProperty));
        }
    }
}