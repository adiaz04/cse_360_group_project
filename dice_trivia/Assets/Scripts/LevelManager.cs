using UnityEngine;
using System.Collections;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public Transform[] boardSpots;
    public Transform spotTypeRed;
    public Transform spotTypeBlue;
    public Player player;
    public QuestionController questionController;
    public GameObject rollButton;
    public GameObject statsManager;
    public Transform playerCameraLocation;
    public Transform diceCameraLocation;
    public Transform dice1;
    public Transform dice2;

    // UI QUIZ TEXT
    public Text quesitonText;
    public Text answerA;
    public Text answerB;
    public Text answerC;
    public Text answerD;

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
        moving,
        movingForward,
        movingBackward,
        question,
        standby
    }


    // Sound Elements
    public AudioClip[] soundFX;
    AudioSource source;
    public bool rolled = false;

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

    statsControl statsfromlevel;

    /*
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
    */

    public GameObject diceMenu;
    public GameObject quizMenu;

    public GameObject statsInARow;
    /// <summary>
    /// Initialze the scene.
    /// </summary>
    void Start()
    {

        statsfromlevel = statsManager.GetComponent<statsControl>();

        source = GetComponent<AudioSource>();
        playSound(4, false, 0f);
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
            }
        }
        else if (CurrentState == State.moving)
        {
            if (!player.Moving)
            {
                CurrentState = State.question;
            }
        }
        else if (CurrentState == State.movingForward || CurrentState == State.movingBackward)
        {
            if (Vector3.Distance(player.transform.position, boardSpots[currentPlayerLocation].position) < 0.5)
            {

                CurrentState = State.timeToRollDice;
            }
        }




        statsfromlevel.playerTotalTime += Time.deltaTime; // Updating the timer
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
        Debug.Log(CurrentState);
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
            case State.question:
                UpdateQuestionUI();
                break;
            case State.standby:
                break;
            default:
                break;
        }
    }

    private void UpdateQuestionUI()
    {
        quizMenu.SetActive(true);
        quesitonText.text = questionController.getQuestion();
        List<Answer> answers = questionController.GetPossibleAnswers();
        answerA.GetComponent<Text>().text = answers[0].GetName();
        answerB.GetComponent<Text>().text = answers[1].GetName();
        answerC.GetComponent<Text>().text = answers[2].GetName();
        answerD.GetComponent<Text>().text = answers[3].GetName();
    }

    private IEnumerator Delay(int seconds)
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
            rollState(false);
            Camera.main.transform.parent = diceCameraLocation;
            Camera.main.transform.position = diceCameraLocation.position;
            Camera.main.transform.rotation = diceCameraLocation.transform.rotation;
        }
        else if (CurrentState == State.idle || CurrentState == State.question || CurrentState == State.moving)
        {
            Camera.main.transform.parent = playerCameraLocation;
            Camera.main.transform.position = playerCameraLocation.position;
            Camera.main.transform.rotation = playerCameraLocation.transform.rotation;
        }
    }

    /// <summary>
    /// Check answer using index.
    /// </summary>
    /// <param name="index"></param>
    public void checkAnswer(int index)
    {
        if (questionController.checkAnswer(index))
        {
            checkAnswer(true);
        }
        else
        {
            checkAnswer(false);
        }
    }

    // Calculating movment based on the quiz answer and the answers history
    public void checkAnswer(bool theAnswer)
    {
        if (!theAnswer)
        {
            playSound(3, false, 0.0f);
            statsfromlevel.playerTotalFalse += 1; // Incrementing total false answers
            quizMenu.SetActive(false);

            if (PlayerInaRow == -2)
            {
                movePlayer((playerLastMove * -1) + 2);
                PlayerInaRow = 0;
            }
            else {
                movePlayer(playerLastMove * -1);
                if (PlayerInaRow < 0)
                {
                    PlayerInaRow += -1;
                }
                else
                {
                    PlayerInaRow = -1;
                }
            }
            CurrentState = State.movingBackward;
        }
        else {

            if (currentPlayerLocation >= boardSpots.Length - 1)
            {
                if (statsfromlevel.playerTotalTime < statsfromlevel.TopTotalTime || statsfromlevel.TopTotalTime == 0.0F)
                    statsfromlevel.SaveData(); // Saving data to file when player reached to the end
                Application.LoadLevel("Finish");
            }
            else {

                playSound(2, false, 0.0f);
                statsfromlevel.playerTotalTrue += 1;
                quizMenu.SetActive(false);

                if (PlayerInaRow == 2)
                {
                    //rrentState = State.movingForward;
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
                CurrentState = State.movingForward;
            }

        }

    }

    /// <summary>
    /// Move Player To specific location.
    /// </summary>
    /// <param name="inSpotsToMove"></param>
    public void movePlayer(int inSpotsToMove)
    {

            move = false;
            if ((currentPlayerLocation + inSpotsToMove) < boardSpots.Length)
                currentPlayerLocation = currentPlayerLocation + inSpotsToMove;
            else
                currentPlayerLocation = boardSpots.Length - 1;
            player.MoveToPosition(boardSpots[currentPlayerLocation]);

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
        CurrentState = State.moving;
        movePlayer(diceResult);
    }


    // Grabbing data both for the current menu and top menun display
    public string GetData(string type, string data)
    {
        if (type == "Current")
        {
            if (data == "Time")
            {
                return statsfromlevel.playerTotalTime.ToString("F2");
            }
            else if (data == "Moves")
            {
                return statsfromlevel.playerTotalTime.ToString();
            }
            else if (data == "Correct")
            {
                return statsfromlevel.playerTotalTrue.ToString();
            }
            else if (data == "False")
            {
                return statsfromlevel.playerTotalFalse.ToString();
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
                return statsfromlevel.TopTotalTime.ToString("F2");
            }
            else if (data == "Moves")
            {
                return statsfromlevel.TopTotalTime.ToString();
            }
            else if (data == "Correct")
            {
                return statsfromlevel.TopTotalTrue.ToString();
            }
            else if (data == "False")
            {
                return statsfromlevel.TopTotalFalse.ToString();
            }
            else
            {
                return "";
            }
        }

    }

    public void rollState(bool state)
    {
        if (!state)
        {
            playSound(0, true, 0.0f);
        }
        else
        {
            playSound(1, false, 1.0f);
        }
    }

    public void playSound(int soundindex, bool loop, float delay)
    {
        source.clip = soundFX[soundindex];
        source.PlayDelayed(delay);
        source.loop = loop;
        source.Play();
    }
}