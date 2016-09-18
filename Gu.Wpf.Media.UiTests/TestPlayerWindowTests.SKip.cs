namespace Gu.Wpf.Media.UiTests
{
    using System;
    using Gu.Wpf.Media.UiTests.Helpers;
    using NUnit.Framework;
    using TestStack.White.UIItems;

    public partial class TestPlayerWindowTests
    {
        public Button SkipForwardButton => this.GetCachedButton();

        public Button SkipForwardTwoButton => this.GetCachedButton();

        public Button SkipForwardZeroPointOneButton => this.GetCachedButton();

        public Button SkipBackButton => this.GetCachedButton();

        public Button SkipBackTwoButton => this.GetCachedButton();

        public Button SkipBackZeroPointOneButton => this.GetCachedButton();

        [Test]
        public void SkipForward()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            Assert.AreEqual(true, this.SkipForwardButton.Enabled);
            Assert.AreEqual(true, this.SkipForwardTwoButton.Enabled);
            Assert.AreEqual(true, this.SkipForwardZeroPointOneButton.Enabled);

            this.SkipForwardButton.Click();
            Assert.AreNotEqual(TimeSpan.FromSeconds(1), this.GetValue(MediaElementWrapper.PositionProperty));
        }

        [Test]
        public void SkipForwardTwo()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            Assert.AreEqual(true, this.SkipForwardButton.Enabled);
            Assert.AreEqual(true, this.SkipForwardTwoButton.Enabled);
            Assert.AreEqual(true, this.SkipForwardZeroPointOneButton.Enabled);

            this.SkipForwardTwoButton.Click();
            Assert.AreNotEqual(TimeSpan.FromSeconds(2), this.GetValue(MediaElementWrapper.PositionProperty));
        }

        [Test]
        public void SkipForwardZeroPointOne()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            Assert.AreEqual(true, this.SkipForwardButton.Enabled);
            Assert.AreEqual(true, this.SkipForwardTwoButton.Enabled);
            Assert.AreEqual(true, this.SkipForwardZeroPointOneButton.Enabled);

            this.SkipForwardZeroPointOneButton.Click();
            Assert.AreNotEqual(TimeSpan.FromSeconds(2), this.GetValue(MediaElementWrapper.PositionProperty));
        }

        [Test]
        public void SkipBack()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.SetValue(MediaElementWrapper.PositionProperty, "00:00:03");
            Assert.AreEqual(true, this.SkipBackButton.Enabled);
            Assert.AreEqual(true, this.SkipBackTwoButton.Enabled);
            Assert.AreEqual(true, this.SkipBackZeroPointOneButton.Enabled);

            this.SkipBackButton.Click();
            Assert.AreNotEqual(TimeSpan.FromSeconds(2), this.GetValue(MediaElementWrapper.PositionProperty));
        }

        [Test]
        public void SkipBackTwo()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.SetValue(MediaElementWrapper.PositionProperty, "00:00:03");
            Assert.AreEqual(true, this.SkipBackButton.Enabled);
            Assert.AreEqual(true, this.SkipBackTwoButton.Enabled);
            Assert.AreEqual(true, this.SkipBackZeroPointOneButton.Enabled);

            this.SkipBackTwoButton.Click();
            Assert.AreNotEqual(TimeSpan.FromSeconds(1), this.GetValue(MediaElementWrapper.PositionProperty));
        }

        [Test]
        public void SkipBackZeroPointOne()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, Info.CoffeeClipFileName());
            this.SetValue(MediaElementWrapper.PositionProperty, "00:00:03");
            Assert.AreEqual(true, this.SkipBackButton.Enabled);
            Assert.AreEqual(true, this.SkipBackTwoButton.Enabled);
            Assert.AreEqual(true, this.SkipBackZeroPointOneButton.Enabled);

            this.SkipBackZeroPointOneButton.Click();
            Assert.AreNotEqual(TimeSpan.FromSeconds(2.9), this.GetValue(MediaElementWrapper.PositionProperty));
        }
    }
}