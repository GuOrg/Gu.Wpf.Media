namespace Gu.Wpf.Media.Tests
{
    using System.Threading;
    using NUnit.Framework;

    public class MediaElementWrapperTests
    {
        [Apartment(ApartmentState.STA)]
        public class Volume
        {
            [Test]
            public void SetVolumeToZeroSetsIsMutedToTrue()
            {
                var wrapper = new MediaElementWrapper();
                Assert.AreEqual(false, wrapper.IsMuted);
                wrapper.SetCurrentValue(MediaElementWrapper.VolumeProperty, 0.0);
                Assert.AreEqual(true, wrapper.IsMuted);
                wrapper.SetCurrentValue(MediaElementWrapper.VolumeProperty, 0.5);
                Assert.AreEqual(false, wrapper.IsMuted);
            }

            [Test]
            public void IsMutedCoercesWhenVolumeIsZero()
            {
                var wrapper = new MediaElementWrapper();
                Assert.AreEqual(false, wrapper.IsMuted);
                wrapper.SetCurrentValue(MediaElementWrapper.VolumeProperty, 0.0);
                Assert.AreEqual(true, wrapper.IsMuted);
                wrapper.SetCurrentValue(MediaElementWrapper.IsMutedProperty, false);
                Assert.AreEqual(true, wrapper.IsMuted);
                wrapper.SetCurrentValue(MediaElementWrapper.VolumeProperty, 0.5);
                Assert.AreEqual(false, wrapper.IsMuted);
            }
        }
    }
}
