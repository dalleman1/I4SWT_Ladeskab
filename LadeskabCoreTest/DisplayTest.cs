using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeskabCore.Display;
using NSubstitute;
using NUnit.Framework;

namespace LadeskabCoreTest
{
    [TestFixture]
    public class DisplayTest
    {
        private IDisplay _display;

        [SetUp]
        public void SetUp()
        {
            _display = Substitute.For<IDisplay>();
        }

        [Test]
        public void ConnectPhone()
        {
            _display.ConnectPhone();
            _display.Received(1).ConnectPhone();
        }

        [Test]
        public void RemovePhone()
        {
            _display.RemovePhone();
            _display.Received(1).RemovePhone();
        }

        [Test]
        public void InsertRFID()
        {
            _display.InsertRFID();
            _display.Received(1).InsertRFID();
        }

        [Test]
        public void ConnectionError()
        {
            _display.ConnectionError();
            _display.Received(1).ConnectionError();
        }

        [Test]
        public void RFIDError()
        {
            _display.RFIDError();
            _display.Received(1).RFIDError();
        }

        [Test]
        public void Charging()
        {
            _display.Charging();
            _display.Received(1).Charging();
        }
    }
    
}