/// <summary>
/// Calculates the score for the user based on time and answers answered corectly and incorectly
/// 
/// @author Jordan Bruno
/// @version 4-10-16
/// </summary>

using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{

    /** Used to hold the player's score */
    public int score;
    /** Used for the players game time */
    public float scoreTime;
    /** Used to see how many answers were answered correctly */
    public int correct;
    /** Used to see how many answers were answered incorrectly */
    public int wrong;
    public string MenuType = "";

    GameObject gameStats;


    /// <summary>
    /// At game start find the Level Manager
    /// </summary>
    void Start()
    {
        gameStats = GameObject.Find("Level Manager");
    }


    /// <summary>
    /// Calls to the level manager to calculate how many questions
    /// were answered correct and incorect. Based on time and questions 
    /// answered the player's score will be calculated.
    /// </summary>
    void Update()
    {
        // Setting scoreTime to the time that the game has run 
        scoreTime += Time.deltaTime;

        // Recieving total correct and wrong from the level manager
        LevelManager temp = gameStats.GetComponent<LevelManager>();
        correct = System.Int32.Parse(temp.GetData("Current", "Correct"));
        wrong = System.Int32.Parse(temp.GetData("Current", "False"));

        // Testing to make sure we are getting the correct values
        //Debug.Log("Correct = " + correct);
        //Debug.Log("Wrong = " + wrong);

        // Calculating the total score
        score = (int)(10000 - (scoreTime + (-correct * 100) + (wrong * 100)));

        //Debug.Log("Score = " + score);
    }



    /// <summary>
    /// Returns the score for the LevelManager
    /// </summary>
    /// <returns>
    /// score Players calculated score
    /// </returns>
    public int getScore()
    {
        return score;
    }
}
