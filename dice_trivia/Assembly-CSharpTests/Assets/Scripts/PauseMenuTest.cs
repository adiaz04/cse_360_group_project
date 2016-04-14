using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture()]
    public class PauseMenuTest
    {
        [Test()]
        public void IsGamePausedTest()
        {
            PauseMenu obj = new PauseMenu();
            obj.debug = true;
            Assert.Equals(true, obj.isPaused);
        }
        [Test()]
        public void ResumeTest()
        {
            PauseMenu obj = new PauseMenu();
            obj.Resume();
            Assert.Equals(false, obj.isPaused);
        }
    }
}