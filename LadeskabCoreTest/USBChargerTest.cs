using LadeskabCore.USBCharger;
using NSubstitute;
using NUnit.Framework;

namespace LadeskabCoreTest
{
    [TestFixture]
    public class USBChargerTest
    {
        private IUsbCharger _usbCharger;

        [SetUp]
        public void SetUp()
        {
            _usbCharger = Substitute.For<UsbChargerSimulator>();
        }

        [Test]
        public void SimulateConnected_Test_DefaultValue()
        {
            Assert.That(_usbCharger.Connected);
        }


    }
}