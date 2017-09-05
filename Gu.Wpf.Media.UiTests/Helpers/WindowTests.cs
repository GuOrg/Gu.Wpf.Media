namespace Gu.Wpf.Media.UiTests.Helpers
{
    using System;
    using System.Collections.Concurrent;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using Gu.Wpf.UiAutomation;
    using NUnit.Framework;
    using Application = Gu.Wpf.UiAutomation.Application;
    using Window = Gu.Wpf.UiAutomation.Window;

    public abstract class WindowTests : IDisposable
    {
        private readonly ConcurrentDictionary<string, AutomationElement> itemCache = new ConcurrentDictionary<string, AutomationElement>();
        private Application application;
        private bool disposed;

        protected Window Window => this.application.MainWindow;

        protected abstract string WindowName { get; }

        public virtual void RestartApplication()
        {
            this.OneTimeTearDown();
            this.itemCache.Clear();
            this.OneTimeSetUp();
        }

        [OneTimeSetUp]
        public virtual void OneTimeSetUp()
        {
            this.application?.Dispose();
            this.application = Application.AttachOrLaunch(Info.CreateStartInfo(this.WindowName));
            ////this.SaveScreenshotToArtifactsDir("start");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ////this.SaveScreenshotToArtifactsDir("finish");
            this.application?.Dispose();
        }

        public void AssertReadOnly(bool expected, DependencyProperty property)
        {
            var textBox = this.GetCachedTextBox(property.Name);
            var readonlyText = expected
                ? "readonly"
                : "editable";
            Assert.AreEqual(expected, !textBox.IsEnabled, $"Expected the property {property.Name} to be {readonlyText}");
        }

        public void AreEqual(string expected, DependencyProperty property)
        {
            var valueBox = this.GetCachedTextBox(property.Name);
            Assert.AreEqual(expected, valueBox.Text, $"Expected the value of {property.Name} to be {expected} was: {valueBox.Text}");
        }

        public TextBox GetCachedTextBox(string name)
        {
            return (TextBox)this.itemCache.GetOrAdd(name, n => this.Window.FindTextBox(n));
        }

        public Button GetCachedButton([CallerMemberName]string name = null)
        {
            Assert.NotNull(name);
            name = name.EndsWith("Button") ? name.TrimEnd("Button") : name;
            return (Button)this.itemCache.GetOrAdd(name, n => this.Window.FindButton(n));
        }

        public void SetValue(DependencyProperty property, string value)
        {
            var valueBox = this.GetCachedTextBox(property.Name);

            if (valueBox.Text != value)
            {
                valueBox.Text = value;
                ////this.Window.WaitWhileBusy();
                //// ReSharper disable once ExplicitCallerInfoArgument
                this.GetCachedButton("Lose focus")
                    .Click();
            }
        }

        public string GetValue(DependencyProperty property)
        {
            var valueBox = this.GetCachedTextBox(property.Name);
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

        // ReSharper disable once UnusedMember.Local
        private void SaveScreenshotToArtifactsDir(string suffix)
        {
            var fileName = System.IO.Path.Combine(Info.ArtifactsDirectory(), $"{this.WindowName}_{suffix}.png");
            using (var image = ScreenCapture.CaptureScreen())
            {
                image.Save(fileName);
            }
        }
    }
}