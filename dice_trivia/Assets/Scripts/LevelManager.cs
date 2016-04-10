using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
    public Transform[] boardSpots;
    public Transform spotTypeRed;
    public Transform spotTypeBlue;
    public Player player;

    public Transform playerCameraLocation;
    public Transform diceCameraLocation;
    public Transform diceRolledCameraLocation;
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


	/// <summary>
    /// Initialze the scene.
    /// </summary>
	void Start () {
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
	void Update () {
	    if (move)
        {
            movePlayer(spotsToMove);
        }
        else if (timeToRollDice)
        {
            CurrentState = State.timeToRollDice;
            timeToRollDice = false;
        }
	}

    /// <summary>
    /// Update game to match current state.
    /// </summary>
    private void StateChanged()
    {
        int valueA = 0;
        int valueB = 0;
        MoveCamera();
        switch (state)
        {
            case State.idle:
                break;
            case State.timeToRollDice:
                dice1.GetComponent<Dice>().Roll();
                dice2.GetComponent<Dice>().Roll();
                //while (valueA == 0 && valueB == 0)
                //{
                    valueA = dice1.GetComponent<Dice>().Value;
                    valueB = dice2.GetComponent<Dice>().Value;
                //};
                
                spotsToMove = valueA + valueB;
                //CurrentState = State.idle;
                move = true;
                break;
            case State.diceRolled:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Move Camera to correct location based on current game state.
    /// </summary>
    private void MoveCamera ()
    {
        if (CurrentState == State.timeToRollDice)
        {
            Camera.main.transform.parent = diceCameraLocation;
            Camera.main.transform.position = diceCameraLocation.position;
            Camera.main.transform.rotation = diceCameraLocation.transform.rotation;
        }
        else if (CurrentState == State.diceRolled)
        {
            Camera.main.transform.parent = diceRolledCameraLocation;
            Camera.main.transform.position = diceRolledCameraLocation.position;
            Camera.main.transform.rotation = diceRolledCameraLocation.transform.rotation;
        }
        else if (CurrentState == State.idle)
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
    private void movePlayer(int inSpotsToMove)
    {
        move = false;
        if ((currentPlayerLocation + inSpotsToMove) < boardSpots.Length)
            currentPlayerLocation = currentPlayerLocation + inSpotsToMove;
        else
            currentPlayerLocation = boardSpots.Length - 1;
        player.MoveToPosition(boardSpots[currentPlayerLocation]);
    }
}
