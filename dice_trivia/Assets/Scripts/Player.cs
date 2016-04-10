using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public Vector3 speed = Vector3.zero;
    public float smoothTime = 0.5F;
    private Transform targetLocation;


    // Added by Itay
    // Avatar parameters
    Animator animator;
    public GameObject playerAvatar;

    // Use this for initialization
    void Start()
    {
        // scene init.

        // Getting the animation component
        animator = playerAvatar.GetComponent<Animator>();
    }

    /// <summary>
    /// If the target has changed since the previous frame, move to the target location.
    /// </summary>
    void Update()
    {
        if (this.targetLocation != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetLocation.position, ref speed, smoothTime);
        }

        if (transform.position != targetLocation.position)
        {
            animator.enabled = true;
            animator.speed = 2.0f;
            animator.Play("Walk");
        }
        else
        {
            animator.enabled = true;
            animator.speed = 2.0f;
            animator.Play("Idle");

        }

    }

    /// <summary>
    /// Sets the target location for the player.
    /// </summary>
    /// <param name="location"></param>



    public void MoveToPosition(Transform location)
    {
        this.targetLocation = location;
    }
}
