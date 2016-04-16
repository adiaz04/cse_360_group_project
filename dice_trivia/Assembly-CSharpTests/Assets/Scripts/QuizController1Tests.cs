using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture()]
    public class QuizController1Tests
    {
        [Test()]
        public void GenerateQuestionsTest()
        {
            string text = "# This is a comment\nQ This is a question\nA This is an incorrect answer\nC This is a correct answer";
            

            QuizController1 test = new QuestionController1();
            test.GenerateQuestions(text);

            Question testQuestion = test.GetQuestion(0);
            Assert.IsNotNull(testQuestion);
            Assert.AreEqual("This is a question", testQuestion.GetName());

            Answer testAnswer1 = test.GetAnswer(0);
            Assert.IsNotNull(testAnswer1);
            Assert.AreEqual("This is an incorrect answer", testAnswer1.GetName());
            Assert.IsFalse(testAnswer1.GetCorrect());

            Answer testAnswer2 = test.GetAnswer(1);
            Assert.IsNotNull(testAnswer2);
            Assert.AreEqual("This is a correct answer", testAnswer2.GetName());
            Assert.IsFalse(testAnswer2.GetCorrect());
        }
    }
}
