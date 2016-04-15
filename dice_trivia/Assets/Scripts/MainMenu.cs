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
        StartCoroutine("FadeOut");
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

    IEnumerator FadeOut()
    {
        float fadeTime = GameObject.Find("Main Menu").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime + 5f);
        Application.LoadLevel(startLevel);

        Debug.Log("Faded Out");
    }

    public void ResetData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = File.Create(Application.dataPath + "/topScore.dat");

        TopScoreData data = new TopScoreData();
        data.totalTime = 9999.99f;
        data.totalMoves = 999;
        data.totalTrue = 0;
        data.totalFalse = 999;

        bf.Serialize(saveFile, data);
        saveFile.Close();
    }
}