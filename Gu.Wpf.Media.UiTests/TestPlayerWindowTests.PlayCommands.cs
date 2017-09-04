namespace Gu.Wpf.Media.UiTests
{
    using System;
    using System.Threading;

    using Gu.Wpf.Media.UiTests.Helpers;
    using Gu.Wpf.UiAutomation;
    using NUnit.Framework;

    public partial class TestPlayerWindowTests
    {
        public Button PlayButton => this.GetCachedButton();

        public Button PauseButton => this.GetCachedButton();

        public Button StopButton => this.GetCachedButton();

        public Button TogglePlayPauseButton => this.GetCachedButton();

        public Button BoundPlayPauseButton => this.GetCachedButton();

        public Button RewindButton => this.GetCachedButton();

        [Test]
        public void ClickPlayThenClickPause()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            var position = this.GetValue(MediaElementWrapper.PositionProperty);
            Assert.AreEqual(true, this.PlayButton.IsEnabled);
            Assert.AreEqual(false, this.PauseButton.IsEnabled);
            this.PlayButton.Click();
            Assert.AreEqual(false, this.PlayButton.IsEnabled);
            Assert.AreEqual(true, this.PauseButton.IsEnabled);
            this.AreEqual("True", MediaElementWrapper.IsPlayingProperty);
            this.AreEqual("Play", MediaElementWrapper.StateProperty);

            Thread.Sleep(TimeSpan.FromSeconds(0.2));

            this.PauseButton.Click();
            Assert.AreEqual(true, this.PlayButton.IsEnabled);
            Assert.AreEqual(false, this.PauseButton.IsEnabled);
            this.AreEqual("False", MediaElementWrapper.IsPlayingProperty);
            this.AreEqual("Pause", MediaElementWrapper.StateProperty);
            Assert.AreNotEqual(position, this.GetValue(MediaElementWrapper.PositionProperty));
        }

        [Test]
        public void TogglePlayPause()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            var position = this.GetValue(MediaElementWrapper.PositionProperty);
            Assert.AreEqual(true, this.PlayButton.IsEnabled);
            Assert.AreEqual(false, this.PauseButton.IsEnabled);
            this.TogglePlayPauseButton.Click();
            Assert.AreEqual(false, this.PlayButton.IsEnabled);
            Assert.AreEqual(true, this.PauseButton.IsEnabled);
            this.AreEqual("True", MediaElementWrapper.IsPlayingProperty);
            this.AreEqual("Play", MediaElementWrapper.StateProperty);

            Thread.Sleep(TimeSpan.FromSeconds(0.2));

            this.TogglePlayPauseButton.Click();
            Assert.AreEqual(true, this.PlayButton.IsEnabled);
            Assert.AreEqual(false, this.PauseButton.IsEnabled);
            this.AreEqual("False", MediaElementWrapper.IsPlayingProperty);
            this.AreEqual("Pause", MediaElementWrapper.StateProperty);
            Assert.AreNotEqual(position, this.GetValue(MediaElementWrapper.PositionProperty));
        }

        [Test]
        public void ToggleBoundPlayPause()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            var position = this.GetValue(MediaElementWrapper.PositionProperty);
            Assert.AreEqual(true, this.PlayButton.IsEnabled);
            Assert.AreEqual(false, this.PauseButton.IsEnabled);
            this.BoundPlayPauseButton.Click();
            Assert.AreEqual(false, this.PlayButton.IsEnabled);
            Assert.AreEqual(true, this.PauseButton.IsEnabled);
            this.AreEqual("True", MediaElementWrapper.IsPlayingProperty);
            this.AreEqual("Play", MediaElementWrapper.StateProperty);

            Thread.Sleep(TimeSpan.FromSeconds(0.2));

            this.BoundPlayPauseButton.Click();
            Assert.AreEqual(true, this.PlayButton.IsEnabled);
            Assert.AreEqual(false, this.PauseButton.IsEnabled);
            this.AreEqual("False", MediaElementWrapper.IsPlayingProperty);
            this.AreEqual("Pause", MediaElementWrapper.StateProperty);
            Assert.AreNotEqual(position, this.GetValue(MediaElementWrapper.PositionProperty));
        }

        [Test]
        public void ClickPlayThenClickStop()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            var position = this.GetValue(MediaElementWrapper.PositionProperty);
            Assert.AreEqual(true, this.PlayButton.IsEnabled);
            Assert.AreEqual(false, this.PauseButton.IsEnabled);
            this.PlayButton.Click();
            Assert.AreEqual(false, this.PlayButton.IsEnabled);
            Assert.AreEqual(true, this.PauseButton.IsEnabled);
            this.AreEqual("True", MediaElementWrapper.IsPlayingProperty);
            this.AreEqual("Play", MediaElementWrapper.StateProperty);

            Thread.Sleep(TimeSpan.FromSeconds(0.2));

            this.StopButton.Click();
            Assert.AreEqual(true, this.PlayButton.IsEnabled);
            Assert.AreEqual(false, this.PauseButton.IsEnabled);
            this.AreEqual("False", MediaElementWrapper.IsPlayingProperty);
            this.AreEqual("Stop", MediaElementWrapper.StateProperty);
            Assert.AreNotEqual(position, this.GetValue(MediaElementWrapper.PositionProperty));
        }

        [Test]
        public void ClickPlayThenClickPauseThenRewind()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            var position = this.GetValue(MediaElementWrapper.PositionProperty);
            Assert.AreEqual(true, this.PlayButton.IsEnabled);
            Assert.AreEqual(false, this.PauseButton.IsEnabled);
            this.PlayButton.Click();
            Assert.AreEqual(false, this.PlayButton.IsEnabled);
            Assert.AreEqual(true, this.PauseButton.IsEnabled);
            this.AreEqual("True", MediaElementWrapper.IsPlayingProperty);
            this.AreEqual("Play", MediaElementWrapper.StateProperty);

            Thread.Sleep(TimeSpan.FromSeconds(0.2));

            this.PauseButton.Click();
            Assert.AreEqual(true, this.PlayButton.IsEnabled);
            Assert.AreEqual(false, this.PauseButton.IsEnabled);
            this.AreEqual("False", MediaElementWrapper.IsPlayingProperty);
            this.AreEqual("Pause", MediaElementWrapper.StateProperty);
            Assert.AreNotEqual(position, this.GetValue(MediaElementWrapper.PositionProperty));

            Assert.AreEqual(true, this.RewindButton.IsEnabled);
            this.RewindButton.Click();
            this.AreEqual("00:00:00", MediaElementWrapper.PositionProperty);
        }
    }
}