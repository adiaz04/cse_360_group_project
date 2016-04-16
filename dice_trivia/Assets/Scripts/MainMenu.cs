using UnityEngine;
using System.Collections;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public string startLevel; //string used for title menu name
    public string statsLevel; //string used to initialize stats database

    /// <summary>
    /// NewGame-Starts a Coroutine for the FadeOut thread
    /// </summary>
	public void NewGame()
    {
        StartCoroutine("FadeOut");
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.R) && Input.GetKeyUp(KeyCode.E) && Input.GetKeyUp(KeyCode.S))
        {
            ResetData();
        }
    }

    /// <summary>
    /// Stats-Calls Unity API to open stats level
    /// </summary>
	public void Stats()
    {
        Application.LoadLevel(statsLevel);
    }

    /// <summary>
    /// QuitGame-Calls Unity API to Quit the Game
    /// </summary>
	public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// FadeOut-thread calls Fading method to fade out of the game
    /// </summary>
    /// <returns>fadeTime-wait time for the thread</returns>
    IEnumerator FadeOut()
    {
        float fadeTime = GameObject.Find("Main Menu").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime + 5f);
        Application.LoadLevel(startLevel);

        Debug.Log("Faded Out");
    }

    /// <summary>
    /// ResetData-calls a formatter to rewrite the data file for stats
    /// </summary>
    public void ResetData()
    {
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream saveFile = File.Create(Application.dataPath + "/topScore.dat");

        //TopScoreData data = new TopScoreData();
        //data.totalTime = 9999.99f;
        //data.totalMoves = 999;
        //data.totalTrue = 0;
        //data.totalFalse = 999;

        //bf.Serialize(saveFile, data);
        //saveFile.Close();
    }
}