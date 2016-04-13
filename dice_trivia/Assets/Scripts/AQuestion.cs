/**
/   
/
/
/
/
*/

using UnityEngine;
using System.Collections;

public class AQuestion : MonoBehaviour
{
    // Setting up some variables
    public QuizController objectReference;
    Question tempQuestion;
    string question;
    Answer answer;
    
    /*// Just being used for testing purposes
    void Start()
    {
        tempQuestion = objectReference.GetQuestion();
        question = tempQuestion.GetName();

        Debug.Log(tempQuestion.GetAnswer(1).GetCorrect());
    } */
    
    // Returning the current question for the levelManager
    public string getQuestion()
    {
        // Retrieving the current question for the level manager
        tempQuestion = objectReference.GetQuestion();
        question = tempQuestion.GetName();

        // Used to check the question for debugging
        Debug.Log("Question Test: " + question);

        // Return the question for the levelManager
        return question;
    }

    // Comparing the users input to the correct answer
    public bool checkAnswer(int userIndex)
    {
        bool isCorrect = false;

        // Retrieving the current question
        tempQuestion = objectReference.GetQuestion();
        question = tempQuestion.GetName();


        // Checking if the user selected the correct answer
        if (tempQuestion.GetAnswer(userIndex).GetCorrect() == true)
        {
            isCorrect = true;
        }

        return isCorrect;
    }
}
