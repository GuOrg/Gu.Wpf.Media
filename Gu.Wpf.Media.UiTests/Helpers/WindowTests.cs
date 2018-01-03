namespace Gu.Wpf.Media.UiTests.Helpers
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using Gu.Wpf.UiAutomation;
    using NUnit.Framework;
    using Application = Gu.Wpf.UiAutomation.Application;
    using Window = Gu.Wpf.UiAutomation.Window;

    public abstract class WindowTests : IDisposable
    {
        private Application application;
        private bool disposed;

        protected Window Window => this.application.MainWindow;

        protected abstract string WindowName { get; }

        public virtual void RestartApplication()
        {
            this.OneTimeTearDown();
            this.OneTimeSetUp();
        }

        [OneTimeSetUp]
        public virtual void OneTimeSetUp()
        {
            this.application?.Dispose();
            this.application = Application.Launch(Application.FindExe("Gu.Wpf.Media.Demo.exe"), this.WindowName);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            this.application?.Dispose();
        }

        public void AssertReadOnly(bool expected, DependencyProperty property)
        {
            var textBox = this.Window.FindTextBox(property.Name);
            var readonlyText = expected
                ? "readonly"
                : "editable";
            Assert.AreEqual(expected, !textBox.IsEnabled, $"Expected the property {property.Name} to be {readonlyText}");
        }

        public void AreEqual(string expected, DependencyProperty property)
        {
            var valueBox = this.Window.FindTextBox(property.Name);
            Assert.AreEqual(expected, valueBox.Text, $"Expected the value of {property.Name} to be {expected} was: {valueBox.Text}");
        }

        public TextBox FindTextBox(string name)
        {
            return this.Window.FindTextBox(name);
        }

        public Button FindButton([CallerMemberName]string name = null)
        {
            Assert.NotNull(name);
            name = name.EndsWith("Button") ? name.TrimEnd("Button") : name;
            return this.Window.FindButton(name);
        }

        public ToggleButton FindToggleButton([CallerMemberName]string name = null)
        {
            Assert.NotNull(name);
            name = name.EndsWith("Button") ? name.TrimEnd("Button") : name;
            return this.Window.FindToggleButton(name);
        }

        public void SetValue(DependencyProperty property, string value)
        {
            var valueBox = this.FindTextBox(property.Name);

            if (valueBox.Text != value)
            {
                valueBox.Text = value;
                ////this.Window.WaitWhileBusy();
                //// ReSharper disable once ExplicitCallerInfoArgument
                this.FindButton("Lose focus")
                    .Click();
            }
        }

        public string GetValue(DependencyProperty property)
        {
            var valueBox = this.FindTextBox(property.Name);
            return valueBox.Text;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            this.disposed = true;
            if (disposing)
            {
                this.application?.Dispose();
            }
        }
    }
}
