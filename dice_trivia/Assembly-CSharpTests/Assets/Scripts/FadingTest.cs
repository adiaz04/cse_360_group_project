using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture()]
    public class FadingTest
    {
        [Test()]
        public void BeginFadeTest()
        {
            Fading obj = new Fading();
            Assert.Equals(0.25f, obj.BeginFade(1));
        }
        [Test()]
        public void BeginFadeTest()
        {
            Fading obj = new Fading();
            Assert.Equals(0.25f, obj.OnLevelWasLoaded());
        }
    }
}