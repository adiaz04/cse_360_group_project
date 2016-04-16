/// <summary>
/// Pulls questions for the levelManager and possible answers
/// 
/// @author Jordan Bruno & Angel Diaz
/// @version 4-10-16
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionController : MonoBehaviour
{
    /** Used to access the quiz controller */
    public QuizController quizController;
    /** Used to hold the question from quizController.GetQuestion() */
    Question question;

    /// <summary>
    /// Returning the current question for the levelManager
    /// </summary>
    /// <returns>
    /// question.GetName() The string of the questions
    /// </returns>
    public string getQuestion()
    {
        // Retrieving the current question for the level manager
        question = quizController.GetQuestion();

        // Used to check the question for debugging
        Debug.Log("Question Test: " + question);

        // Return the question for the levelManager
        return question.GetName();
    }

    /// <summary>
    /// Gets a list of possible answers for the levelManager
    /// </summary>
    /// <returns>
    /// answers Possible answers
    /// </returns>
    public List<Answer> GetPossibleAnswers()
    {
        List<Answer> answers = question.GetPossibleAnswers();
        return answers;
    }

    /// <summary>
    /// Comparing the users input to the correct answer
    /// </summary>
    /// <param name="userIndex"></param>
    /// <returns>
    /// isCorrect Boolean value to see if the user inputed answer was correct
    /// </returns>
    public bool checkAnswer(int userIndex)
    {
        /** Boolean to check if the user inputed answer is correct */
        bool isCorrect = false;

        // Checking if the user selected the correct answer
        if (question.GetAnswer(userIndex).GetCorrect() == true)
        {
            isCorrect = true;
        }

        return isCorrect;
    }
}
