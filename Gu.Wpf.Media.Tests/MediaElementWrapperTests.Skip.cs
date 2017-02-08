// ReSharper disable SuppressSetMutable
namespace Gu.Wpf.Media.Tests
{
    using System.Threading;
    using NUnit.Framework;

    public partial class MediaElementWrapperTests
    {
        [Apartment(ApartmentState.STA)]
        public class SKip
        {
            ////private MediaElementWrapper wrapper;
            ////private ManualResetEvent mre = new ManualResetEvent(false);

            ////[OneTimeSetUp]
            ////public void SetUp()
            ////{
            ////    this.wrapper = new MediaElementWrapper
            ////    {
            ////        LoadedBehavior = Gu.Wpf.Media.MediaState.Pause,
            ////    };

            ////    this.wrapper.BeginInit();
            ////    this.wrapper.Source = Clips.CoffeeClipUri();
            ////    this.wrapper.Loaded += this.OnLoaded;
            ////    this.mre.WaitOne();
            ////}

            ////[TestCase(0.1, 0.1)]
            ////[TestCase(-0.1, -0.1)]
            ////[TestCase("", 1)]
            ////[TestCase("0.1", 0.1)]
            ////[TestCase("1", 1)]
            ////[TestCase("2", 2)]
            ////[TestCase("00:00:02", 2)]
            ////[TestCase("abc", 0)]
            ////[TestCase(null, 1)]
            ////[TestCase(true, 0)]
            ////[TestCase(1, 1)]
            ////[TestCase(-1, -1)]
            ////public void SkipForward(object parameter, double expected)
            ////{
            ////    var startTime = TimeSpan.FromSeconds(3);
            ////    this.wrapper.Position = startTime;
            ////    Assert.AreEqual(TimeSpan.Zero, this.wrapper.Position);
            ////    Assert.AreEqual(true, Commands.SkipForward.CanExecute(parameter, this.wrapper));
            ////    Commands.SkipForward.Execute(parameter, this.wrapper);
            ////    Assert.AreEqual(startTime + TimeSpan.FromSeconds(expected), this.wrapper.Position);
            ////}

            ////private void OnLoaded(object sender, RoutedEventArgs e)
            ////{
            ////    this.wrapper.Loaded -= this.OnLoaded;
            ////    this.mre.Set();
            ////}
        }
    }
}