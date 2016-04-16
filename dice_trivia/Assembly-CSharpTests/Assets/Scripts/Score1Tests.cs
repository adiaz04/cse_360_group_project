using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture()]
    public class Score1Tests
    {
        [Test()]
        public void UpdateTest()
        {
            Score1 test = new Score1();
            test.Update();

            Assert.AreEqual(2, test.correct);
            Assert.AreEqual(2, test.wrong);
            Assert.AreEqual(9900, test.score);
        }

        [Test()]
        public void getScoreTest()
        {
            Score1 test = new Score1();
            test.Update();

            Assert.AreEqual(9900, test.getScore());
        }
    }
}