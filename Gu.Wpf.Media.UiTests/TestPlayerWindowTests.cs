namespace Gu.Wpf.Media.UiTests
{
    using Gu.Wpf.Media.UiTests.Helpers;
    using NUnit.Framework;

    public partial class TestPlayerWindowTests : WindowTests
    {
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
    }
}
