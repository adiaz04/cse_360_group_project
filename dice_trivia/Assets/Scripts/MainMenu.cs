using UnityEngine;
using System.Collections;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public string startLevel;

	public string statsLevel;

	public void NewGame()
	{
		Application.LoadLevel (startLevel);
	}

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.R) && Input.GetKeyUp(KeyCode.E) && Input.GetKeyUp(KeyCode.S))
        {
            ResetData();
        }
    }

	public void Stats()
	{
        Application.LoadLevel (statsLevel);
	}

	public void QuitGame()
	{
		Application.Quit ();
	}

    public void ResetData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = File.Create(Application.dataPath + "/topScore.dat");

        TopScoreData data = new TopScoreData();
        data.totalTime = 99.99f;
        data.totalMoves = 0;
        data.totalTrue = 0;
        data.totalFalse = 0;

        bf.Serialize(saveFile, data);
        saveFile.Close();
    }
}