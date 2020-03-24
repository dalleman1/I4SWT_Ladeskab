using LadeskabCore.ChargeControl;
using LadeskabCore.Display;
using LadeskabCore.Door;
using LadeskabCore.RFIDReader;
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
        private IDisplay _display;
        private IRFIDReader _reader;

        [SetUp]
        public void SetUp()
        {
            _stationControl = Substitute.For<IStationControl>();
            _door = Substitute.For<IDoor>();
            _chargeControl = Substitute.For<IChargeControl>();
            _display = Substitute.For<IDisplay>();
            _reader = Substitute.For<IRFIDReader>();

            _stationControl = new StationControl();
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
            Assert.That(_stationControl.Isconnected());
        }

        [Test]
        public void ChargeMessage_Test_displayCorrect()
        {
            
        }

        [Test]
        public void fuckyou()
        {
            _reader.OnDetectEvent() += Raise.EventWith(new RFIDDetectedEventsArgs() { ID = 123 });
            Assert.That((_stationControl.));
        }



    }
}
