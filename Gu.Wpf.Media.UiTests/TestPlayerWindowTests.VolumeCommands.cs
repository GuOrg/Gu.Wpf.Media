namespace Gu.Wpf.Media.UiTests
{
    using System;
    using System.Threading;

    using Gu.Wpf.Media.UiTests.Helpers;

    using NUnit.Framework;

    using TestStack.White.UIItems;

    public partial class TestPlayerWindowTests
    {
        public Button ToggleMuteButton => this.Window.Get<Button>("ToggleMute");

        public Button BoundToggleMuteButton => this.Window.Get<Button>("BoundToggleMute");

        public Button MuteVolumeButton => this.Window.Get<Button>("MuteVolume");

        public Button DecreaseVolumeButton => this.Window.Get<Button>("DecreaseVolume");

        public Button IncreaseVolumeButton => this.Window.Get<Button>("IncreaseVolume");

        [Test]
        public void ClickMuteThenIncreaseVolume()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
            this.AreEqual("0.5", MediaElementWrapper.VolumeProperty);
            Assert.AreEqual(true, this.MuteVolumeButton.Enabled);
            Assert.AreEqual(true, this.DecreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.Enabled);

            this.MuteVolumeButton.Click();
            this.AreEqual("True", MediaElementWrapper.IsMutedProperty);
            Assert.AreEqual(false, this.MuteVolumeButton.Enabled);
            Assert.AreEqual(false, this.DecreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.Enabled);

            this.IncreaseVolumeButton.Click();
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
            this.AreEqual("0.05", MediaElementWrapper.VolumeProperty);
            Assert.AreEqual(true, this.MuteVolumeButton.Enabled);
            Assert.AreEqual(true, this.DecreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.Enabled);
        }

        [Test]
        public void ClickMuteThenToggleMute()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
            this.AreEqual("0.5", MediaElementWrapper.VolumeProperty);
            Assert.AreEqual(true, this.MuteVolumeButton.Enabled);
            Assert.AreEqual(true, this.DecreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.Enabled);

            this.MuteVolumeButton.Click();
            this.AreEqual("True", MediaElementWrapper.IsMutedProperty);
            Assert.AreEqual(false, this.MuteVolumeButton.Enabled);
            Assert.AreEqual(false, this.DecreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.Enabled);

            this.ToggleMuteButton.Click();
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
            this.AreEqual("0.5", MediaElementWrapper.VolumeProperty);
            Assert.AreEqual(true, this.MuteVolumeButton.Enabled);
            Assert.AreEqual(true, this.DecreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.Enabled);
        }

        [Test]
        public void ClickToggleMuteButtonTwice()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
            Assert.AreEqual(true, this.MuteVolumeButton.Enabled);
            Assert.AreEqual(true, this.DecreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.Enabled);

            this.ToggleMuteButton.Click();
            this.AreEqual("True", MediaElementWrapper.IsMutedProperty);
            Assert.AreEqual(false, this.MuteVolumeButton.Enabled);
            Assert.AreEqual(false, this.DecreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.Enabled);

            this.ToggleMuteButton.Click();
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
            Assert.AreEqual(true, this.MuteVolumeButton.Enabled);
            Assert.AreEqual(true, this.DecreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.Enabled);
        }

        [Test]
        public void ClickBoundToggleMuteButtonTwice()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
            Assert.AreEqual(true, this.MuteVolumeButton.Enabled);
            Assert.AreEqual(true, this.DecreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.Enabled);

            this.BoundToggleMuteButton.Click();
            this.AreEqual("True", MediaElementWrapper.IsMutedProperty);
            Assert.AreEqual(false, this.MuteVolumeButton.Enabled);
            Assert.AreEqual(false, this.DecreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.Enabled);

            this.BoundToggleMuteButton.Click();
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
            Assert.AreEqual(true, this.MuteVolumeButton.Enabled);
            Assert.AreEqual(true, this.DecreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.Enabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.Enabled);
        }

        [Test]
        public void IncreaseVolume()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.AreEqual("0.5", MediaElementWrapper.VolumeProperty);
            this.IncreaseVolumeButton.Click();
            this.AreEqual("0.55", MediaElementWrapper.VolumeProperty);

            this.SetValue(MediaElementWrapper.VolumeIncrementProperty, "0.2");
            this.IncreaseVolumeButton.Click();
            this.AreEqual("0.75", MediaElementWrapper.VolumeProperty);

            this.IncreaseVolumeButton.Click();
            this.AreEqual("0.95", MediaElementWrapper.VolumeProperty);

            this.IncreaseVolumeButton.Click();
            this.AreEqual("1", MediaElementWrapper.VolumeProperty);
            Assert.AreEqual(false, this.IncreaseVolumeButton.Enabled);
        }

        [Test]
        public void DecreaseVolume()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.AreEqual("0.5", MediaElementWrapper.VolumeProperty);
            this.DecreaseVolumeButton.Click();
            this.AreEqual("0.45", MediaElementWrapper.VolumeProperty);

            this.SetValue(MediaElementWrapper.VolumeIncrementProperty, "0.2");
            this.DecreaseVolumeButton.Click();
            this.AreEqual("0.25", MediaElementWrapper.VolumeProperty);

            this.DecreaseVolumeButton.Click();
            this.AreEqual("0.05", MediaElementWrapper.VolumeProperty);

            this.DecreaseVolumeButton.Click();
            this.AreEqual("0", MediaElementWrapper.VolumeProperty);
            Assert.AreEqual(false, this.DecreaseVolumeButton.Enabled);
            this.AreEqual("True", MediaElementWrapper.IsMutedProperty);

            this.IncreaseVolumeButton.Click();
            this.AreEqual("0.2", MediaElementWrapper.VolumeProperty);
            Assert.AreEqual(true, this.IncreaseVolumeButton.Enabled);
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
        }

        [Test]
        public void ToggleMuteDisabledWhenVolumeIsZero()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.SetValue(MediaElementWrapper.VolumeProperty, "0");
            Assert.AreEqual(false, this.MuteVolumeButton.Enabled);
            Assert.AreEqual(false, this.ToggleMuteButton.Enabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.Enabled);
            Assert.AreEqual(false, this.DecreaseVolumeButton.Enabled);
            this.AreEqual("True", MediaElementWrapper.IsMutedProperty);

            this.BoundToggleMuteButton.Click();
            Assert.AreEqual(false, this.MuteVolumeButton.Enabled);
            Assert.AreEqual(false, this.ToggleMuteButton.Enabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.Enabled);
            Assert.AreEqual(false, this.DecreaseVolumeButton.Enabled);
            this.AreEqual("True", MediaElementWrapper.IsMutedProperty);

            this.SetValue(MediaElementWrapper.VolumeProperty, "0.5");
            Assert.AreEqual(true, this.MuteVolumeButton.Enabled);
            Assert.AreEqual(true, this.ToggleMuteButton.Enabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.Enabled);
            Assert.AreEqual(true, this.DecreaseVolumeButton.Enabled);
        }
    }
}