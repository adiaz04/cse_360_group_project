using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour {

    public Transform player;
    public Vector3 targetPosition;
    public float speed = 4.0f;
    public float height = 2.0f;
    public float radius = 10.0f;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = new Vector3(player.position.x, player.position.y + 0.8f, player.position.z + 7.0f);
        transform.position = (transform.position - player.position).normalized * radius + player.position;
	}
	
    /// <summary>
    /// Last update runs after each frame update, allowing the camera following to happen after player is done moving.
    /// </summary>
	void LateUpdate () {
        transform.RotateAround(player.position, Vector3.up, speed * Time.deltaTime);
        transform.LookAt(player.position);
        targetPosition = (transform.position - player.position).normalized * radius + player.position;
        targetPosition.y = height;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime + speed);
    }
}
