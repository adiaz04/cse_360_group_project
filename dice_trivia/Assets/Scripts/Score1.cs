/** Calculates the score for the user based on time and answers answered corectly and incorectly
*   
*   @author Jordan Bruno
*   @version 4-10-16
*/

using UnityEngine;
using System.Collections;

public class Score1
{

    // Initializing some variables
    public int score;
    public float scoreTime;
    public int correct;
    public int wrong;
    public string MenuType = "";

    //GameObject gameStats;


    /** At game start find the level manager */
    public void Start()
    {
        //gameStats = GameObject.Find("Level Manager");
    }


    /** Update is called once per frame. Calls to the level manager to 
    *   calculate how many questions were answered correct and incorect.
    *   Based on time and questions answered the player's score will be calculated.
    */
    public void Update()
    {
        // Setting scoreTime to the time that the game has run 
        scoreTime = 100;     //+= Time.deltaTime;

        // Recieving total correct and wrong from the level manager
        // LevelManager temp = gameStats.GetComponent<LevelManager>();
        correct = 2;          //System.Int32.Parse(temp.GetData("Current", "Correct"));
        wrong = 2;            //System.Int32.Parse(temp.GetData("Current", "False"));

        // Testing to make sure we are getting the correct values
        //Debug.Log("Correct = " + correct);
        //Debug.Log("Wrong = " + wrong);

        // Calculating the total score
        score = (int)(10000 - (scoreTime + (-correct * 100) + (wrong * 100)));

        //Debug.Log("Score = " + score);
    }



    /** Returns score for the levelManager 
    *
    * @return score
    */
    public int getScore()
    {
        return score;
    }
}
