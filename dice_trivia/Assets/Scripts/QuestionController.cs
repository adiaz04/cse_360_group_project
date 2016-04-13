/**
/   
/
/
/
/
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionController : MonoBehaviour
{
    // Setting up some variables
    public QuizController quizController;
    Question question;

    /// <summary>
    /// Returning the current question for the levelManager
    /// </summary>
    /// <returns></returns>
    public string getQuestion()
    {
        // Retrieving the current question for the level manager
        question = quizController.GetQuestion();

        // Used to check the question for debugging
        Debug.Log("Question Test: " + question);

        // Return the question for the levelManager
        return question.GetName();
    }

    public List<Answer> GetPossibleAnswers()
    {
        List<Answer> answers = question.GetPossibleAnswers();
        return answers;
    }

    /// <summary>
    /// Comparing the users input to the correct answer
    /// </summary>
    /// <param name="userIndex"></param>
    /// <returns></returns>
    public bool checkAnswer(int userIndex)
    {
        bool isCorrect = false;

        // Checking if the user selected the correct answer
        if (question.GetAnswer(userIndex).GetCorrect() == true)
        {
            isCorrect = true;
        }

        return isCorrect;
    }
}
