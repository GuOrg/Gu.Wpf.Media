// ReSharper disable SuppressSetMutable
namespace Gu.Wpf.Media.Tests
{
    using System.Threading;
    using System.Windows.Controls;
    using System.Windows.Input;

    using NUnit.Framework;

    [Apartment(ApartmentState.STA)]
    public static class VolumeTests
    {
        [Test]
        public static void DefaultValue()
        {
            var wrapper = new MediaElementWrapper();
            var element = new MediaElement();
            Assert.AreEqual(element.Volume, wrapper.Volume);
        }

        [Test]
        public static void SetVolumeToZeroSetsIsMutedToTrue()
        {
            var wrapper = new MediaElementWrapper();
            Assert.AreEqual(false, wrapper.IsMuted);
            wrapper.Volume = 0;
            Assert.AreEqual(true, wrapper.IsMuted);
            wrapper.Volume = 0.5;
            Assert.AreEqual(false, wrapper.IsMuted);
        }

        [Test]
        public static void IsMutedCoercesWhenVolumeIsZero()
        {
            var wrapper = new MediaElementWrapper();
            Assert.AreEqual(false, wrapper.IsMuted);
            wrapper.Volume = 0.0;
            Assert.AreEqual(true, wrapper.IsMuted);
            wrapper.IsMuted = false;
            Assert.AreEqual(true, wrapper.IsMuted);
            wrapper.Volume = 0.5;
            Assert.AreEqual(false, wrapper.IsMuted);
        }

        [TestCase(0.1, 0.6)]
        [TestCase(-0.1, 0.4)]
        [TestCase("", 0.5)]
        [TestCase("0.1", 0.6)]
        [TestCase("abc", 0.5)]
        [TestCase(null, 0.55)]
        [TestCase(true, 0.5)]
        [TestCase(1, 1)]
        [TestCase(1u, 1)]
        [TestCase(-1, 0)]
        public static void IncreaseVolumeWithParameter(object increment, double expected)
        {
            var wrapper = new MediaElementWrapper();
            Assert.AreEqual(0.5, wrapper.Volume);
            Assert.AreEqual(0.05, wrapper.VolumeIncrement);
            MediaCommands.IncreaseVolume.Execute(increment, wrapper);
            Assert.AreEqual(expected, wrapper.Volume);
        }

        [TestCase(0.1, 0.4)]
        [TestCase(-0.1, 0.6)]
        [TestCase("", 0.5)]
        [TestCase("0.1", 0.4)]
        [TestCase("abc", 0.5)]
        [TestCase(true, 0.5)]
        [TestCase(null, 0.45)]
        [TestCase(1, 0)]
        [TestCase(-1, 1)]
        public static void DecreaseVolumeWithParameter(object increment, double expected)
        {
            var wrapper = new MediaElementWrapper();
            Assert.AreEqual(0.5, wrapper.Volume);
            Assert.AreEqual(0.05, wrapper.VolumeIncrement);
            MediaCommands.DecreaseVolume.Execute(increment, wrapper);
            Assert.AreEqual(expected, wrapper.Volume);
        }
    }
}
