using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture()]
    public class LevelManager1Tests
    {
        [Test()]
        public void StartGameTest()
        {
            LevelManager1 levelManger = new LevelManager1();
            levelManger.Start();
            Assert.AreEqual(LevelManager1.State.idle, levelManger.CurrentState);
        }

        [Test()]
        public void checkAnswerTest()
        {
            LevelManager1 levelManger = new LevelManager1();
            levelManger.checkAnswer(false);
            Assert.AreEqual(LevelManager1.State.movingBackward, levelManger.CurrentState);
        }
    }
}