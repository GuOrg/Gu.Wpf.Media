namespace Gu.Wpf.Media.UiTests
{
    using System;
    using Gu.Wpf.Media.UiTests.Helpers;
    using Gu.Wpf.UiAutomation;
    using NUnit.Framework;

    public partial class TestPlayerWindowTests
    {
        public Button SkipForwardButton => this.FindButton();

        public Button SkipForwardTwoButton => this.FindButton();

        public Button SkipForwardZeroPointOneButton => this.FindButton();

        public Button SkipBackButton => this.FindButton();

        public Button SkipBackTwoButton => this.FindButton();

        public Button SkipBackZeroPointOneButton => this.FindButton();

        [Test]
        public void SkipForward()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            Assert.AreEqual(true, this.SkipForwardButton.IsEnabled);
            Assert.AreEqual(true, this.SkipForwardTwoButton.IsEnabled);
            Assert.AreEqual(true, this.SkipForwardZeroPointOneButton.IsEnabled);

            this.SkipForwardButton.Click();
            Assert.AreNotEqual(TimeSpan.FromSeconds(1), this.GetValue(MediaElementWrapper.PositionProperty));
        }

        [Test]
        public void SkipForwardTwo()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            Assert.AreEqual(true, this.SkipForwardButton.IsEnabled);
            Assert.AreEqual(true, this.SkipForwardTwoButton.IsEnabled);
            Assert.AreEqual(true, this.SkipForwardZeroPointOneButton.IsEnabled);

            this.SkipForwardTwoButton.Click();
            Assert.AreNotEqual(TimeSpan.FromSeconds(2), this.GetValue(MediaElementWrapper.PositionProperty));
        }

        [Test]
        public void SkipForwardZeroPointOne()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            Assert.AreEqual(true, this.SkipForwardButton.IsEnabled);
            Assert.AreEqual(true, this.SkipForwardTwoButton.IsEnabled);
            Assert.AreEqual(true, this.SkipForwardZeroPointOneButton.IsEnabled);

            this.SkipForwardZeroPointOneButton.Click();
            Assert.AreNotEqual(TimeSpan.FromSeconds(2), this.GetValue(MediaElementWrapper.PositionProperty));
        }

        [Test]
        public void SkipBack()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.SetValue(MediaElementWrapper.PositionProperty, "00:00:03");
            Assert.AreEqual(true, this.SkipBackButton.IsEnabled);
            Assert.AreEqual(true, this.SkipBackTwoButton.IsEnabled);
            Assert.AreEqual(true, this.SkipBackZeroPointOneButton.IsEnabled);

            this.SkipBackButton.Click();
            Assert.AreNotEqual(TimeSpan.FromSeconds(2), this.GetValue(MediaElementWrapper.PositionProperty));
        }

        [Test]
        public void SkipBackTwo()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.SetValue(MediaElementWrapper.PositionProperty, "00:00:03");
            Assert.AreEqual(true, this.SkipBackButton.IsEnabled);
            Assert.AreEqual(true, this.SkipBackTwoButton.IsEnabled);
            Assert.AreEqual(true, this.SkipBackZeroPointOneButton.IsEnabled);

            this.SkipBackTwoButton.Click();
            Assert.AreNotEqual(TimeSpan.FromSeconds(1), this.GetValue(MediaElementWrapper.PositionProperty));
        }

        [Test]
        public void SkipBackZeroPointOne()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.SetValue(MediaElementWrapper.PositionProperty, "00:00:03");
            Assert.AreEqual(true, this.SkipBackButton.IsEnabled);
            Assert.AreEqual(true, this.SkipBackTwoButton.IsEnabled);
            Assert.AreEqual(true, this.SkipBackZeroPointOneButton.IsEnabled);

            this.SkipBackZeroPointOneButton.Click();
            Assert.AreNotEqual(TimeSpan.FromSeconds(2.9), this.GetValue(MediaElementWrapper.PositionProperty));
        }
    }
}