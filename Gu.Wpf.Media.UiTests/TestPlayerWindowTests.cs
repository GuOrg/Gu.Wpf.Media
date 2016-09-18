namespace Gu.Wpf.Media.UiTests
{
    using System.Collections.Concurrent;
    using System.Windows;

    using Gu.Wpf.Media.UiTests.Helpers;

    using NUnit.Framework;

    using TestStack.White.UIItems;

    public partial class TestPlayerWindowTests : WindowTests
    {
        private readonly ConcurrentDictionary<string, IUIItem> itemCache = new ConcurrentDictionary<string, IUIItem>();

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

        public override void RestartApplication()
        {
            this.itemCache.Clear();
            base.RestartApplication();
        }

        public void AreEqual(string expected, bool @readonly, DependencyProperty property)
        {
            this.GetCachedTextBox("SelectedPropertyNameTextBox").Text = property.Name;
            var textBox = this.GetCachedTextBox("ValueTextBox");
            var readonlyText = @readonly
                        ? "readonly"
                        : "editabl";
            Assert.AreEqual(@readonly, !textBox.Enabled, $"Expected the property {property.Name} to be {readonlyText}");
            Assert.AreEqual(expected, textBox.Text, $"Expected the value of {property.Name} to be {expected} was: {textBox.Text}");
        }

        public void AreEqual(string expected, DependencyProperty property)
        {
            this.GetCachedTextBox("SelectedPropertyNameTextBox").Text = property.Name;
            var valueBox = this.GetCachedTextBox("ValueTextBox");
            Assert.AreEqual(expected, valueBox.Text, $"Expected the value of {property.Name} to be {expected} was: {valueBox.Text}");
        }

        private void SetValue(DependencyProperty property, string value)
        {
            this.GetCachedTextBox("SelectedPropertyNameTextBox").Text = property.Name;
            var valueBox = this.GetCachedTextBox("ValueTextBox");

            if (valueBox.Text != value)
            {
                valueBox.BulkText = value;
                this.Window.WaitWhileBusy();
                this.GetCachedButton("LoseFocusButton")
                        .Click();
            }
        }

        private string GetValue(DependencyProperty property)
        {
            this.GetCachedTextBox("SelectedPropertyNameTextBox").Text = property.Name;
            var valueBox = this.GetCachedTextBox("ValueTextBox");
            return valueBox.Text;
        }

        public TextBox GetCachedTextBox(string name)
        {
            return this.GetCached<TextBox>(name);
        }

        public Button GetCachedButton(string name)
        {
            return this.GetCached<Button>(name);
        }

        public T GetCached<T>(string name) where T : IUIItem
        {
            return (T)this.itemCache.GetOrAdd(name, n => this.Window.Get<T>(n));
        }
    }
}