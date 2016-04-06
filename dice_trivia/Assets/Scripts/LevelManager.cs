using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
    public Transform[] boardSpots;
    public Transform spotTypeRed;
    public Transform spotTypeBlue;
    public Player player;
    public int numberOfSpots = 60;
    public bool move = false;
    public int spotsToMove = 6;
    private int currentPlayerLocation;

	/// <summary>
    /// Initialze the scene.
    /// </summary>
	void Start () {
	    for (int index = 0; index < numberOfSpots; index++)
        {
            boardSpots[index] = (Transform)Instantiate(spotTypeBlue, new Vector3(index, 0, 0), Quaternion.identity);
        }
        player.transform.position = boardSpots[0].transform.position;
        
	}
	
	/// <summary>
    /// Called once every frame, used to update the scene.
    /// </summary>
	void Update () {
	    if (move)
        {
            movePlayer(spotsToMove);
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
