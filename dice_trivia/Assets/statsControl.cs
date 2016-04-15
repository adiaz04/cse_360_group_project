using UnityEngine;
using System.Collections;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;

public class statsControl : MonoBehaviour {


    // Current player statistics
    public float playerTotalTime = 0.0f;
    public int playerTotalMoves = 0;
    public int playerTotalTrue = 0;
    public int playerTotalFalse = 0;
    // Top plater statistics
    public float TopTotalTime = 0.0f;
    public int TopTotalMoves = 0;
    public int TopTotalTrue = 0;
    public int TopTotalFalse = 0;


    // Use this for initialization
    void Start () {
        LoadData(); // Loading top score statistics from file
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    public string GetData(string type, string data)
    {
        if (type == "Current")
        {
            if (data == "Time")
            {
                return playerTotalTime.ToString("F2");
            }
            else if (data == "Moves")
            {
                return playerTotalTime.ToString();
            }
            else if (data == "Correct")
            {
                return playerTotalTrue.ToString();
            }
            else if (data == "False")
            {
                return playerTotalFalse.ToString();
            }
            else if (data == "IAR")
            {
                return "";
            }
            else
            {
                return "";
            }
        }
        else
        {
            if (data == "Time")
            {
                return TopTotalTime.ToString("F2");
            }
            else if (data == "Moves")
            {
                return TopTotalTime.ToString();
            }
            else if (data == "Correct")
            {
                return TopTotalTrue.ToString();
            }
            else if (data == "False")
            {
                return TopTotalFalse.ToString();
            }
            else
            {
                return "";
            }
        }
    }


    public void RestartGame()
    {
        playerTotalTime = 0;
        playerTotalTrue = 0;
        playerTotalFalse = 0;
        Application.LoadLevel("title_screen");
    }
    


    // Save player's data intofile
    public void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = File.Create(Application.dataPath + "/topScore.dat");

        TopScoreData data = new TopScoreData();
        data.totalTime = playerTotalTime;
        data.totalMoves = playerTotalMoves;
        data.totalTrue = playerTotalTrue;
        data.totalFalse = playerTotalFalse;

        bf.Serialize(saveFile, data);
        saveFile.Close();
    }

    // Loads top score stats from file
    public void LoadData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = File.Open(Application.dataPath + "/topScore.dat", FileMode.Open);
        TopScoreData data = (TopScoreData)bf.Deserialize(saveFile);
        saveFile.Close();

        TopTotalTime = data.totalTime;
        TopTotalMoves = data.totalMoves;
        TopTotalTrue = data.totalTrue;
        TopTotalFalse = data.totalFalse;

    }

}

// TopScoreData class
[Serializable]
class TopScoreData
{
    public float totalTime;
    public int totalMoves;
    public int totalTrue;
    public int totalFalse;
}
