using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture()]
    public class QuestionController1Tests
    {
        [Test()]
        public void getQuestionTest()
        {
            QuestionController1 test = new QuestionController1();

            string stringTest = ("This is a test");

            Assert.AreEqual(stringTest, test.getQuestion());
        }

        [Test()]
        public void GetPossibleAnswersTest()
        {
            QuestionController1 test = new QuestionController1();

            List<string> answers = new List<string>();
            answers.Add("test 1");
            answers.Add("test 2");
            answers.Add("test 3");
            answers.Add("test 4");

            Assert.AreEqual(answers, test.GetPossibleAnswers());
        }

        [Test()]
        public void checkAnswerTest()
        {
            QuestionController1 test = new QuestionController1();

            bool trueBool = true;
            bool falseBool = false;

            Assert.AreEqual(trueBool, test.checkAnswer(1));
            Assert.AreEqual(falseBool, test.checkAnswer(0));
        }
    }
}