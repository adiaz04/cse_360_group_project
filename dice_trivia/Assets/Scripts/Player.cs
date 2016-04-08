using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public Vector3 speed = Vector3.zero;
    public float smoothTime = 0.3F;
    private Transform targetLocation;

	// Use this for initialization
	void Start () {
        // scene init.
	}
	
	/// <summary>
    /// If the target has changed since the previous frame, move to the target location.
    /// </summary>
	void Update () {
        if (this.targetLocation != null)
            transform.position = Vector3.SmoothDamp(transform.position, targetLocation.position, ref speed, smoothTime);
	}

    /// <summary>
    /// Sets the target location for the player.
    /// </summary>
    /// <param name="location"></param>
    public void MoveToPosition (Transform location)
    {
        this.targetLocation = location;
    }
}
