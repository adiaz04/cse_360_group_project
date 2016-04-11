﻿using UnityEngine;
using System.Collections;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Transform[] boardSpots;
    public Transform spotTypeRed;
    public Transform spotTypeBlue;
    public Player player;
    public GameObject rollButton;
    public Transform playerCameraLocation;
    public Transform diceCameraLocation;
    public Transform dice1;
    public Transform dice2;
    public int numberOfSpots = 60;
    public bool move = false;
    public bool timeToRollDice = false;
    public int spotsToMove = 0;
    private int currentPlayerLocation;
    private Camera mainCamera;

    // Camera Movement
    public float cameraMovement = 0;
    public float cameraSpeed = 0.8f;
    private Transform targetLocation;
    private State state;
    enum State
    {
        idle = 0,
        timeToRollDice,
        diceRolled,
        question,
        standby
    }

    /// <summary>
    /// Used to keep track of current state.
    /// </summary>
    private State CurrentState
    {
        get
        {
            return state;
        }
        set
        {
            this.state = value;
            StateChanged();
        }
    }




    int PlayerInaRow = 0;
    int playerLastMove = 0;

    // Current player statistics
    float playerTotalTime = 0.0f;
    int playerTotalMoves = 0;
    int playerTotalTrue = 0;
    int playerTotalFalse = 0;
    // Top plater statistics
    float TopTotalTime = 0.0f;
    int TopTotalMoves = 0;
    int TopTotalTrue = 0;
    int TopTotalFalse = 0;


    public GameObject diceMenu;
    public GameObject quizMenu;

    public GameObject statsInARow;
    /// <summary>
    /// Initialze the scene.
    /// </summary>
    void Start()
    {

        LoadData(); // Loading top score statistics from file 

        quizMenu.SetActive(false); // Hiding the DEV quiz panel
        rollButton.SetActive(false);
        for (int index = 0; index < numberOfSpots; index++)
        {
            boardSpots[index] = (Transform)Instantiate(spotTypeBlue, new Vector3(index, 0, 0), Quaternion.identity);
        }
        player.transform.position = boardSpots[0].transform.position;
        Camera.main.transform.parent = playerCameraLocation;
        Camera.main.transform.position = playerCameraLocation.position;
        Camera.main.transform.rotation = playerCameraLocation.transform.rotation;
        this.state = State.idle;
    }

    /// <summary>
    /// Called once every frame, used to update the scene.
    /// </summary>
    void Update()
    {
        if (CurrentState == State.diceRolled)
        {
            if (dice1.GetComponent<Dice>().Value != 0 && dice2.GetComponent<Dice>().Value != 0)
            {
                int value = dice1.GetComponent<Dice>().Value + dice2.GetComponent<Dice>().Value;
                SetLastMove(value);
                dice1.GetComponent<Dice>().Reset();
                dice2.GetComponent<Dice>().Reset();
                CurrentState = State.question;
            }
        }
        playerTotalTime += Time.deltaTime; // Updating the timer
        timeToRollDice = false;
    }

    public void StartGame()
    {
        this.CurrentState = State.timeToRollDice;
    }
    /// <summary>
    /// Update game to match current state.
    /// </summary>
    private void StateChanged()
    {
        MoveCamera();
        switch (state)
        {
            case State.idle:
                break;
            case State.timeToRollDice:
                rollButton.SetActive(true);
                break;
            case State.diceRolled:
                rollButton.SetActive(false);
                break;
            case State.standby:
                break;
            default:
                break;
        }
    }

    private IEnumerator Delay (int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
    /// <summary>
    /// Move Camera to correct location based on current game state.
    /// </summary>
    private void MoveCamera()
    {
        if (CurrentState == State.timeToRollDice)
        {
            Camera.main.transform.parent = diceCameraLocation;
            Camera.main.transform.position = diceCameraLocation.position;
            Camera.main.transform.rotation = diceCameraLocation.transform.rotation;
        }
        else if (CurrentState == State.idle || CurrentState == State.question)
        {
            Camera.main.transform.parent = playerCameraLocation;
            Camera.main.transform.position = playerCameraLocation.position;
            Camera.main.transform.rotation = playerCameraLocation.transform.rotation;
        }
    }


    /// <summary>
    /// Move player appropriate number of spaces.
    /// </summary>
    /// <param name="inSpotsToMove"></param>
    /// 

    // Moved to the Player prefab


    // Calculating movment based on the quiz answer and the answers history
    public void checkAnswer(bool theAnswer)
    {
        if (!theAnswer)
        {
            playerTotalFalse += 1; // Incrementing total false answers

            if (PlayerInaRow == -2)
            {
                movePlayer((playerLastMove * -1) + 2);
                PlayerInaRow = 0;
                diceMenu.SetActive(true);
            }
            else {
                movePlayer(playerLastMove * -1);
                if (PlayerInaRow > 0)
                {
                    PlayerInaRow = -1;
                }
                else
                {
                    PlayerInaRow -= 1;
                    diceMenu.SetActive(true);
                }
            }
        }
        else {
            playerTotalTrue += 1;

            if (PlayerInaRow == 2)
            {
                movePlayer(2);
                PlayerInaRow = 0;
            }
            else {
                if (PlayerInaRow < 0)
                {
                    PlayerInaRow = 1;
                }
                else
                {
                    PlayerInaRow += 1;
                }
            }
        }
        CurrentState = State.timeToRollDice;
    }

    /// <summary>
    /// Move Player To specific location.
    /// </summary>
    /// <param name="inSpotsToMove"></param>
    public void movePlayer(int inSpotsToMove)
    {

        if (currentPlayerLocation >= boardSpots.Length && playerTotalTime < TopTotalTime)
        {
            SaveData(); // Saving data to file when player reached to the end
        }
        else
        {
            move = false;
            if ((currentPlayerLocation + inSpotsToMove) < boardSpots.Length)
                currentPlayerLocation = currentPlayerLocation + inSpotsToMove;
            else
                currentPlayerLocation = boardSpots.Length - 1;
            player.MoveToPosition(boardSpots[currentPlayerLocation]);
            Debug.Log("moving player forward: " + inSpotsToMove);
            if (inSpotsToMove > 0)
            {
                quizMenu.SetActive(true);
                diceMenu.SetActive(false);
            }
            else {
                quizMenu.SetActive(false);
                diceMenu.SetActive(true);
            }
        }

    }

    public void RollDice()
    {
        dice1.GetComponent<Dice>().Roll();
        dice2.GetComponent<Dice>().Roll();
        CurrentState = State.diceRolled;
    }

    public void SetLastMove(int diceResult)
    {
        playerLastMove = diceResult;
        movePlayer(diceResult);
    }


    // Grabbing data both for the current menu and top menun display
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
                return PlayerInaRow.ToString();
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