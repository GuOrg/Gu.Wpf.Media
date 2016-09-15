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
            this.RestartApplication();
            this.AreEqual("", false, MediaElementWrapper.BalanceProperty);
            this.AreEqual("", true, MediaElementWrapper.BufferingProgressProperty);
            this.AreEqual("", false, MediaElementWrapper.SourceProperty);
        }

        public void AreEqual(string expected, bool @readonly, DependencyProperty property)
        {
            var groupBox = this.Window.GetByText<GroupBox>("SelectedProperty");
            groupBox.Get<TextBox>("SelectedPropertyNameTextBox").Text = property.Name;
            var textBox = groupBox.Get<TextBox>("ValueTextBox");
            Assert.AreEqual(@readonly, !textBox.Enabled);
            Assert.AreEqual(expected, textBox.Text);
        }
    }
}