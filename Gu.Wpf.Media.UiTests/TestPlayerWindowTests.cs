namespace Gu.Wpf.Media.UiTests
{
    using System.Collections.Concurrent;
    using System.Runtime.CompilerServices;
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
            this.SetValue(MediaElementWrapper.SourceProperty, string.Empty);
            this.SetValue(MediaElementWrapper.VolumeProperty, "0.5");
            this.SetValue(MediaElementWrapper.VolumeIncrementProperty, "0.05");
        }

        public override void RestartApplication()
        {
            this.itemCache.Clear();
            base.RestartApplication();
        }

        public void AssertReadOnly(bool expected, DependencyProperty property)
        {
            var textBox = this.GetCachedTextBox(property.Name);
            var readonlyText = expected
                ? "readonly"
                : "editable";
            Assert.AreEqual(expected, !textBox.Enabled, $"Expected the property {property.Name} to be {readonlyText}");
        }

        public void AreEqual(string expected, DependencyProperty property)
        {
            var valueBox = this.GetCachedTextBox(property.Name);
            Assert.AreEqual(expected, valueBox.Text, $"Expected the value of {property.Name} to be {expected} was: {valueBox.Text}");
        }

        public TextBox GetCachedTextBox(string name)
        {
            return this.GetCached<TextBox>(name);
        }

        public Button GetCachedButton([CallerMemberName]string name = null)
        {
            Assert.NotNull(name);
            return this.GetCached<Button>(name.EndsWith("Button") ? name.TrimEnd("Button") : name);
        }

        public T GetCached<T>(string name)
            where T : IUIItem
        {
            return (T)this.itemCache.GetOrAdd(name, n => this.Window.Get<T>(n));
        }

        private void SetValue(DependencyProperty property, string value)
        {
            var valueBox = this.GetCachedTextBox(property.Name);

            if (valueBox.Text != value)
            {
                valueBox.BulkText = value;
                ////this.Window.WaitWhileBusy();
                //// ReSharper disable once ExplicitCallerInfoArgument
                this.GetCachedButton("Lose focus")
                    .Click();
            }
        }

        private string GetValue(DependencyProperty property)
        {
            var valueBox = this.GetCachedTextBox(property.Name);
            return valueBox.Text;
        }
    }
}