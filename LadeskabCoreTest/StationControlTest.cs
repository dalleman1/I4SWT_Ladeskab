using LadeskabCore.ChargeControl;
using LadeskabCore.Door;
using LadeskabCore.StationControl;
using NSubstitute;
using NUnit.Framework;

namespace LadeskabCoreTest
{
    [TestFixture]
    public class StationControlTest
    {
        private IStationControl _stationControl;
        private IDoor _door;
        private IChargeControl _chargeControl;

        [SetUp]
        public void SetUp()
        {
            _stationControl = Substitute.For<IStationControl>();
            _door = Substitute.For<IDoor>();
            _chargeControl = Substitute.For<IChargeControl>();

        }

        [Test]
        public void IsConnected_Test_True()
        {
            _chargeControl.IsConnected().Returns(true);
            Assert.IsTrue(_stationControl.Isconnected());
        }

        [Test]
        public void IsConnected_Test_False()
        {
            _chargeControl.IsConnected().Returns(false);
            Assert.IsFalse(_stationControl.Isconnected());
        }

        [Test]
        public void test()
        {
         
        }

    }
}
