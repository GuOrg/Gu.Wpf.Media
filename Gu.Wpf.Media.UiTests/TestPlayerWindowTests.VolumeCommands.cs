namespace Gu.Wpf.Media.UiTests
{
    using Gu.Wpf.Media.UiTests.Helpers;
    using Gu.Wpf.UiAutomation;
    using NUnit.Framework;

    public partial class TestPlayerWindowTests
    {
        private Button ToggleMuteButton => this.FindButton();

        private Button BoundToggleMuteButton => this.FindButton();

        private Button MuteVolumeButton => this.FindButton();

        private Button UnmuteVolumeButton => this.FindButton();

        private Button DecreaseVolumeButton => this.FindButton();

        private Button DecreaseVolumeByOneButton => this.FindButton();

        private Button DecreaseVolumeByZeroPointOneButton => this.FindButton();

        private Button IncreaseVolumeButton => this.FindButton();

        private Button IncreaseVolumeByOneButton => this.FindButton();

        private Button IncreaseVolumeByZeroPointOneButton => this.FindButton();

        [Test]
        public void ClickMuteThenIncreaseVolume()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
            this.AreEqual("0.5", MediaElementWrapper.VolumeProperty);
            Assert.AreEqual(true, this.MuteVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.DecreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.IsEnabled);

            this.MuteVolumeButton.Click();
            this.AreEqual("True", MediaElementWrapper.IsMutedProperty);
            Assert.AreEqual(false, this.MuteVolumeButton.IsEnabled);
            Assert.AreEqual(false, this.DecreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.IsEnabled);

            this.IncreaseVolumeButton.Click();
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
            this.AreEqual("0.05", MediaElementWrapper.VolumeProperty);
            Assert.AreEqual(true, this.MuteVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.DecreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.IsEnabled);
        }

        [Test]
        public void ClickMuteThenToggleMute()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
            this.AreEqual("0.5", MediaElementWrapper.VolumeProperty);
            Assert.AreEqual(true, this.MuteVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.DecreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.IsEnabled);

            this.MuteVolumeButton.Click();
            this.AreEqual("True", MediaElementWrapper.IsMutedProperty);
            Assert.AreEqual(false, this.MuteVolumeButton.IsEnabled);
            Assert.AreEqual(false, this.DecreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.IsEnabled);

            this.ToggleMuteButton.Click();
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
            this.AreEqual("0.5", MediaElementWrapper.VolumeProperty);
            Assert.AreEqual(true, this.MuteVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.DecreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.IsEnabled);
        }

        [Test]
        public void ClickMuteThenUnmute()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
            this.AreEqual("0.5", MediaElementWrapper.VolumeProperty);
            Assert.AreEqual(true, this.MuteVolumeButton.IsEnabled);
            Assert.AreEqual(false, this.UnmuteVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.DecreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.IsEnabled);

            this.MuteVolumeButton.Click();
            this.AreEqual("True", MediaElementWrapper.IsMutedProperty);
            Assert.AreEqual(false, this.MuteVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.UnmuteVolumeButton.IsEnabled);
            Assert.AreEqual(false, this.DecreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.IsEnabled);

            this.UnmuteVolumeButton.Click();
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
            this.AreEqual("0.5", MediaElementWrapper.VolumeProperty);
            Assert.AreEqual(true, this.MuteVolumeButton.IsEnabled);
            Assert.AreEqual(false, this.UnmuteVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.DecreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.IsEnabled);
        }

        [Test]
        public void ClickToggleMuteButtonTwice()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
            Assert.AreEqual(true, this.MuteVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.DecreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.IsEnabled);

            this.ToggleMuteButton.Click();
            this.AreEqual("True", MediaElementWrapper.IsMutedProperty);
            Assert.AreEqual(false, this.MuteVolumeButton.IsEnabled);
            Assert.AreEqual(false, this.DecreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.IsEnabled);

            this.ToggleMuteButton.Click();
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
            Assert.AreEqual(true, this.MuteVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.DecreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.IsEnabled);
        }

        [Test]
        public void ClickBoundToggleMuteButtonTwice()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
            Assert.AreEqual(true, this.MuteVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.DecreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.IsEnabled);

            this.BoundToggleMuteButton.Click();
            this.AreEqual("True", MediaElementWrapper.IsMutedProperty);
            Assert.AreEqual(false, this.MuteVolumeButton.IsEnabled);
            Assert.AreEqual(false, this.DecreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.IsEnabled);

            this.BoundToggleMuteButton.Click();
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
            Assert.AreEqual(true, this.MuteVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.DecreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.IncreaseVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.IsEnabled);
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
            Assert.AreEqual(false, this.IncreaseVolumeButton.IsEnabled);
        }

        [Test]
        public void IncreaseVolumeByOne()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.AreEqual("0.5", MediaElementWrapper.VolumeProperty);
            this.IncreaseVolumeByOneButton.Click();
            this.AreEqual("1", MediaElementWrapper.VolumeProperty);
        }

        [Test]
        public void IncreaseVolumeByZeroPointOne()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.AreEqual("0.5", MediaElementWrapper.VolumeProperty);
            this.IncreaseVolumeByZeroPointOneButton.Click();
            this.AreEqual("0.6", MediaElementWrapper.VolumeProperty);
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
            Assert.AreEqual(false, this.DecreaseVolumeButton.IsEnabled);
            this.AreEqual("True", MediaElementWrapper.IsMutedProperty);

            this.IncreaseVolumeButton.Click();
            this.AreEqual("0.2", MediaElementWrapper.VolumeProperty);
            Assert.AreEqual(true, this.IncreaseVolumeButton.IsEnabled);
            this.AreEqual("False", MediaElementWrapper.IsMutedProperty);
        }

        [Test]
        public void DecreaseVolumeByOne()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.AreEqual("0.5", MediaElementWrapper.VolumeProperty);
            this.DecreaseVolumeByOneButton.Click();
            this.AreEqual("0", MediaElementWrapper.VolumeProperty);
        }

        [Test]
        public void DecreaseVolumeByZeroPointOne()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.AreEqual("0.5", MediaElementWrapper.VolumeProperty);
            this.DecreaseVolumeByZeroPointOneButton.Click();
            this.AreEqual("0", MediaElementWrapper.VolumeProperty);
        }

        [Test]
        public void ToggleMuteDisabledWhenVolumeIsZero()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.SetValue(MediaElementWrapper.VolumeProperty, "0");
            Assert.AreEqual(false, this.MuteVolumeButton.IsEnabled);
            Assert.AreEqual(false, this.ToggleMuteButton.IsEnabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.IsEnabled);
            Assert.AreEqual(false, this.DecreaseVolumeButton.IsEnabled);
            this.AreEqual("True", MediaElementWrapper.IsMutedProperty);

            this.BoundToggleMuteButton.Click();
            Assert.AreEqual(false, this.MuteVolumeButton.IsEnabled);
            Assert.AreEqual(false, this.ToggleMuteButton.IsEnabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.IsEnabled);
            Assert.AreEqual(false, this.DecreaseVolumeButton.IsEnabled);
            this.AreEqual("True", MediaElementWrapper.IsMutedProperty);

            this.SetValue(MediaElementWrapper.VolumeProperty, "0.5");
            Assert.AreEqual(true, this.MuteVolumeButton.IsEnabled);
            Assert.AreEqual(true, this.ToggleMuteButton.IsEnabled);
            Assert.AreEqual(true, this.BoundToggleMuteButton.IsEnabled);
            Assert.AreEqual(true, this.DecreaseVolumeButton.IsEnabled);
        }
    }
}