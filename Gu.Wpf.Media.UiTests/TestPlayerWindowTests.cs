namespace Gu.Wpf.Media.UiTests
{
    using System.Windows;

    using Gu.Wpf.Media.UiTests.Helpers;

    using NUnit.Framework;

    using TestStack.White.UIItems;

    public class TestPlayerWindowTests : WindowTests
    {
        protected override string WindowName { get; } = "TestPlayerWindow";

        [Test]
        public void Loads()
        {
            this.SetValue(MediaElementWrapper.SourceProperty, "");
            this.AreEqual("0", false, MediaElementWrapper.BalanceProperty);
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
            this.AreEqual("0.5", false, MediaElementWrapper.VolumeProperty);
            this.AreEqual("0.05", false, MediaElementWrapper.VolumeIncrementProperty);
        }

        public void AreEqual(string expected, bool @readonly, DependencyProperty property)
        {
            var groupBox = this.Window.GetByText<GroupBox>("SelectedProperty");
            groupBox.Get<TextBox>("SelectedPropertyNameTextBox").Text = property.Name;
            var textBox = groupBox.Get<TextBox>("ValueTextBox");
            var readonlyText = @readonly
                        ? "readonly"
                        : "editabl";
            Assert.AreEqual(@readonly, !textBox.Enabled, $"Expected the property {property.Name} to be {readonlyText}");
            Assert.AreEqual(expected, textBox.Text, $"Expected the value of {property.Name} to be {expected} was: {textBox.Text}");
        }

        private void SetValue(DependencyProperty property, string value)
        {
            var groupBox = this.Window.GetByText<GroupBox>("SelectedProperty");
            groupBox.Get<TextBox>("SelectedPropertyNameTextBox").Text = property.Name;
            var textBox = groupBox.Get<TextBox>("ValueTextBox");
            textBox.BulkText = value;
            this.PressTab();
        }
    }
}