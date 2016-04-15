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
        public void getScoreTest()
        {
            Console.WriteLine("Hello");
            int scoreTest;

            Score1 test = new Score1();
            test.Start();
            test.Update();

            scoreTest = test.getScore();

            // Making sure test is not null
            Assert.IsNotNull(test);

            // Checking to make sure the variables initialize correctly
            Assert.AreEqual(test.correct, 0);
            Assert.AreEqual(test.wrong, 0);

            // Checking to see that the score is greater that 0
            Assert.Greater(scoreTest, 0);
        }
    }
}