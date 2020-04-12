using LadeskabCore.ChargeControl;
using LadeskabCore.Display;
using LadeskabCore.Door;
using LadeskabCore.RFIDReader;
using LadeskabCore.StationControl;
using LadeskabCore.USBCharger;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using System.Threading;

namespace LadeskabCoreTest
{
    [TestFixture]
    public class StationControlTest
    {
        private IDoor _door;
        private IChargeControl _chargeControl;
        private IDisplay _display;
        private IRFIDReader _reader;
        private StationControl control = new StationControl();

        [SetUp]
        public void SetUp()
        {
            _door = Substitute.For<Door>();
            _chargeControl = Substitute.For<IChargeControl>();
            _display = Substitute.For<Display>();
            _reader = Substitute.For<IRFIDReader>();

        }

        [Test]
        public void IsConnected_Test_True()
        {
            _chargeControl.IsConnected().Equals(true);
            Assert.That(control.Isconnected());
        }

        [Test]
        public void IsConnected_Test_False()
        {
            _chargeControl.IsConnected().Equals(false);
            Assert.IsFalse(control.Isconnected().Equals(false));
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

        [Test]
        public void ChargeMessage_Test_ChargingCalled()
        {
            control.ChargeMessage();
            _display.Received(1).Charging();

        }

        [Test]
        public void LockDoor_Test_LockCalled()
        {
            control.LockDoor();
            _door.Received(1).Lock();
        }

        [Test]
        public void UnlockDoor_Test_LockCalled()
        {
            control.UnlockDoor();
            _door.Received(1).Unlock();
        }

        [Test]
        public void StartCharge_Test()
        {
            control.StartCharge(); // This raises an event that sets ID to 123.
            Assert.IsTrue(control._ID == 123);
        }

        [Test]
        public void CheckID_Set_Test()
        {
            control.CheckID(123, 123);

            Assert.IsTrue(control._ID == 1 || control._oldID == 123);
        }

        [Test]
        public void CheckID_Available_Test()
        {
            control.SetCabinState(StationControl.CabinState.Available);
            control.CheckID(123, 123);
            Assert.IsTrue(control.GetCabinState() == StationControl.CabinState.Locked);
        }

        [Test]
        public void CheckID_DoorOpen_Test()
        {
            control.SetCabinState(StationControl.CabinState.DoorOpen);
            control.CheckID(123, 123);
            Assert.IsTrue(control.GetCabinState() == StationControl.CabinState.DoorOpen);
        }

        [Test]
        public void CheckID_Locked_Error_Test()
        {
            control.SetCabinState(StationControl.CabinState.Locked);
            control.CheckID(123, 100);
            Assert.IsTrue(control.GetCabinState() == StationControl.CabinState.Locked);
        }
        [Test]
        public void CheckID_Locked_NotError_Test()
        {
            control.SetCabinState(StationControl.CabinState.Locked);
            control.CheckID(123, 123);
            Assert.IsTrue(control.GetCabinState() == StationControl.CabinState.Available);
        }

        [Test]
        public void SetCabinState_Test()
        {
            control.SetCabinState(StationControl.CabinState.Locked);

            Assert.That(control.GetCabinState() == StationControl.CabinState.Locked);
        }
    }
}
