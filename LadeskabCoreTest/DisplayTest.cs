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
            public void printtest()
            {
                
            }

    }
    
}