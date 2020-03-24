using LadeskabCore.ChargeControl;
using LadeskabCore.Display;
using LadeskabCore.Door;
using LadeskabCore.RFIDReader;
using LadeskabCore.StationControl;
using LadeskabCore.USBCharger;
using System.Collections.Generic;
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
        private StationControl control;

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
            _chargeControl.IsConnected().Equals(true);
            Assert.That(_stationControl.Isconnected());
        }

        [Test]
        public void IsConnected_Test_False()
        {
            _chargeControl.IsConnected().Equals(false);
            Assert.IsFalse(_stationControl.Isconnected().Equals(false));
        }

        [Test]
        public void RFIDDetectedEvent_Test()
        {
            List<int> receivedEvents = new List<int>();
            RFIDReader reader = new RFIDReader();

            reader.RaiseDetectEvent += delegate (object sender, RFIDDetectedEventsArgs e)
            {
                receivedEvents.Add(e.ID);
            };

            reader.RaiseRandomEvent(123);
            Assert.AreEqual(123, receivedEvents[0]);
        }

        [Test]
        public void DoorTriggeredEvent_Test()
        {
            List<DoorStates> receivedEvents = new List<DoorStates>();
            Door door = new Door();

            door.DoorChangedEvent += delegate (object sender, DoorTriggeredEventArgs e)
            {
                receivedEvents.Add(e._doorstate);
            };

            door.Lock();
            door.Unlock();

            Assert.AreEqual(2, receivedEvents.Count);
            Assert.AreEqual(DoorStates.DoorLocked, receivedEvents[0]);
            Assert.AreEqual(DoorStates.DoorUnlocked, receivedEvents[1]);
        }

        [Test]
       
        public void ChargeTriggeredEvent_Test()
        {
            List<System.Tuple<ChargeStates, double>> receivedEvents = new List<System.Tuple<ChargeStates, double>>();
            ChargeControl chargeControl = new ChargeControl();

            chargeControl.RaisedChargeEvent += delegate (object sender, ChargeTriggeredEventArgs e)
            {
                receivedEvents.Add(System.Tuple.Create(e._chargeState, chargeControl.usb.CurrentValue));
            };

            chargeControl.StartCharge();
            chargeControl.StopCharge();

            Assert.That(receivedEvents[0].Item1, Is.EqualTo(ChargeStates.Charging));
            Assert.That(receivedEvents[0].Item2, Is.EqualTo(System.Convert.ToDouble(500)));

            Assert.That(receivedEvents[1].Item1, Is.EqualTo(ChargeStates.NoConnection));
            Assert.That(receivedEvents[1].Item2, Is.LessThan(System.Convert.ToDouble(500)));
        }
    }
}
