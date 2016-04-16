using UnityEngine;
using System.Collections;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Globalization;

public class statsControl : MonoBehaviour {


    // Current player statistics
    static float playerTotalTime = 0.0f;
    public int playerTotalMoves = 0;
    static int playerTotalTrue = 0;
    static int playerTotalFalse = 0;
    // Top plater statistics
    public float TopTotalTime = 0.0f;
    public int TopTotalMoves = 0;
    public int TopTotalTrue = 0;
    public int TopTotalFalse = 0;

    // Server connection info
    private string secretKey = "mySecretKey"; // Edit this value and make sure it's the same as the one stored on the server
    public string addScoreURL = "http://dice.ib-zone.com/setData.php?"; //be sure to add a ? to your url
    public string highscoreURL = "http://dice.ib-zone.com/getData.php";


    // Use this for initialization
    void Start () {
        LoadData(); // Loading top score statistics from file
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public void setTime(float time)
    {
        playerTotalTime = time;
    }

    public float getTime()
    {
        return playerTotalTime;
    }



    public void setTotalTrue(int totalcorrect)
    {
        playerTotalTrue = totalcorrect;
    }

    public int getTotalTrue()
    {
        return playerTotalTrue;
    }


    public void setTotalFalse(int totalfalse)
    {
        playerTotalFalse = totalfalse;
    }

    public int getTotalFalse()
    {
        return playerTotalFalse;
    }




    void Awake()
    {
        DontDestroyOnLoad(this);
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
        LoadData();
        Application.LoadLevel("title_screen");
    }
    


    // Save player's data intofile
    public void SaveData()
    {
        StartCoroutine(PostScores(playerTotalTime.ToString("F2"), playerTotalTrue, playerTotalFalse));
    }

    // Loads top score stats from file
    public void LoadData()
    {
        StartCoroutine(GetScores());
    }

    IEnumerator PostScores(string name, int totalCorrect, int totalFalse)
    {
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
        string hash = Md5Sum(name + totalCorrect + totalFalse + secretKey);

        string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&totalCorrect=" + totalCorrect + "&totalFalse=" + totalFalse + "&hash=" + hash;

        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            print("Error Getting Data: " + hs_post.error);
        }
    }

    // Get the scores from the MySQL DB to display in a GUIText.
    // remember to use StartCoroutine when calling this function!
    IEnumerator GetScores()
    {
        WWW hs_get = new WWW(highscoreURL);
        yield return hs_get;

        if (hs_get.error != null)
        {
            print("Error Getting Data: " + hs_get.error);
        }
        else
        {
            string tmpstring = "";
            tmpstring = hs_get.text; // this is a GUIText that will display the scores in game.
            string[] split = tmpstring.Split('\t');
            TopTotalTime = float.Parse(split[0]);
            TopTotalTrue = int.Parse(split[1]);
            TopTotalFalse = int.Parse(split[2]);
        }
    }

    public string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }

}
