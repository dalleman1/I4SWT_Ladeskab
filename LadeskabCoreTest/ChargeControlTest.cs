using LadeskabCore.ChargeControl;
using LadeskabCore.USBCharger;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace LadeskabCoreTest
{
    [TestFixture]
    public class ChargeControlTest
    {
        private IUsbCharger _usbCharger;
        private IChargeControl _chargeControl;

        [SetUp]
        public void SetUp()
        {
            _usbCharger = Substitute.For<IUsbCharger>();
            _chargeControl = Substitute.For<ChargeControl>();
        }

        [Test]
        public void usbStartCharge_Test_Called()
        {
            _usbCharger.StartCharge();
            _chargeControl.Received(1).StartCharge();
        }

        [Test]
        public void usbStopCharge_Test_Called()
        {
            _usbCharger.StopCharge();
            _chargeControl.Received(1).StopCharge();
        }

        [Test]
        public void usbConnected_Test_IsTrue()
        {
            _usbCharger.Connected.Equals(true);
            Assert.That(_chargeControl.IsConnected());
        }

        [Test]
        public void usbConnected_Test_IsFalse()
        {
            _usbCharger.Connected.Equals(false);
            Assert.IsFalse(_chargeControl.IsConnected().Equals(false));
        }

        // Test that our class does indeed fire an event
        [Test]
        public void HandleChargeEvent_Test()
        {
            bool ChargeControlUpdated = false;
            ManualResetEvent ChargeControlUpdatedEvent = new ManualResetEvent(false);

            _chargeControl.RaisedChargeEvent += delegate
            {
                ChargeControlUpdated = true;
                ChargeControlUpdatedEvent.Set();
            };

            _chargeControl.StartCharge();

            ChargeControlUpdatedEvent.WaitOne(5000, false);

            Assert.IsTrue(ChargeControlUpdated);
        }
    }
}