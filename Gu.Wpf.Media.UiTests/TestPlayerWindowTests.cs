namespace Gu.Wpf.Media.UiTests
{
    using System.Windows;

    using Gu.Wpf.Media.UiTests.Helpers;

    using NUnit.Framework;

    using TestStack.White.UIItems;

    public partial class TestPlayerWindowTests : WindowTests
    {
        protected override string WindowName { get; } = "TestPlayerWindow";

        [SetUp]
        public void SetUp()
        {
            this.SetValue(MediaElementWrapper.StateProperty, "Pause");
            this.SetValue(MediaElementWrapper.LoadedBehaviorProperty, "Pause");
            this.SetValue(MediaElementWrapper.SourceProperty, "");
            this.SetValue(MediaElementWrapper.VolumeProperty, "0.5");
            this.SetValue(MediaElementWrapper.VolumeIncrementProperty, "0.05");
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

        public void AreEqual(string expected, DependencyProperty property)
        {
            var groupBox = this.Window.GetByText<GroupBox>("SelectedProperty");
            groupBox.Get<TextBox>("SelectedPropertyNameTextBox").Text = property.Name;
            var textBox = groupBox.Get<TextBox>("ValueTextBox");
            Assert.AreEqual(expected, textBox.Text, $"Expected the value of {property.Name} to be {expected} was: {textBox.Text}");
        }

        private void SetValue(DependencyProperty property, string value)
        {
            var groupBox = this.Window.GetByText<GroupBox>("SelectedProperty");
            groupBox.Get<TextBox>("SelectedPropertyNameTextBox").Text = property.Name;
            var textBox = groupBox.Get<TextBox>("ValueTextBox");

            if (textBox.Text != value)
            {
                textBox.BulkText = value;
                this.Window.WaitWhileBusy();
                groupBox.Get<Button>("LoseFocusButton")
                        .Click();
            }
        }

        private string GetValue(DependencyProperty property)
        {
            var groupBox = this.Window.GetByText<GroupBox>("SelectedProperty");
            groupBox.Get<TextBox>("SelectedPropertyNameTextBox").Text = property.Name;
            var textBox = groupBox.Get<TextBox>("ValueTextBox");
            return textBox.Text;
        }
    }
}