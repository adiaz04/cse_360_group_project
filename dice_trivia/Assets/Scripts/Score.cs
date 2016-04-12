using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

    // Initializing some variables
    public int score;
    public float scoreTime;
    public int correct;
    public int wrong;
    public string MenuType = "";

    GameObject gameStats;


    void Start()
    {
        gameStats = GameObject.Find("Level Manager");
    }


	// Update is called once per frame
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
        score = (int)(10000 - (scoreTime + (-correct*100) + (wrong*100)));

        //Debug.Log("Score = " + score);
    }

    
    public int getScore()
    {
        return score;
    }
}
