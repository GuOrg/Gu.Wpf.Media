namespace Gu.Wpf.Media.UiTests.Helpers
{
    using System;
    using Gu.Wpf.UiAutomation;
    using NUnit.Framework;

    public abstract class WindowTests : IDisposable
    {
        private Application application;
        private bool disposed;

        public static Window StaticWindow { get; private set; }

        protected Window Window => StaticWindow;

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
            this.application = Application.AttachOrLaunch(Info.CreateStartInfo(this.WindowName));
            StaticWindow = this.application.MainWindow;
            ////this.SaveScreenshotToArtifactsDir("start");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ////this.SaveScreenshotToArtifactsDir("finish");
            this.application?.Dispose();
            StaticWindow = null;
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