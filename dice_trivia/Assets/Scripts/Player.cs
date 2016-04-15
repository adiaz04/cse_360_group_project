using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public Vector3 speed = Vector3.zero;
    public float smoothTime = 0.3F;
    public bool Moving
    {
        get { return this.moving; }
    }
    private bool moving = false;
    private Transform targetLocation;
    public bool finishScreen = false;


    // Added by Itay
    // Avatar parameters
    Animator animator;
    public GameObject playerAvatar;

    // Use this for initialization
    void Start()
    {
        // scene init.
        targetLocation = transform;
        // Getting the animation component
        animator = playerAvatar.GetComponent<Animator>();
    }

    /// <summary>
    /// If the target has changed since the previous frame, move to the target location.
    /// </summary>
    void Update()
    {

        if (finishScreen)
        {
            moving = true;
            animator.enabled = true;
            animator.speed = 2.0f;
            animator.Play("Jump");
        }
        else
        {
            if (this.targetLocation != null)
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetLocation.position, ref speed, smoothTime);
            }

            if (Vector3.Distance(transform.position, targetLocation.position) > 0.5)
            {
                moving = true;
                animator.enabled = true;
                animator.speed = 2.0f;
                animator.Play("Walk");

            }
            else
            {
                animator.enabled = true;
                animator.speed = 2.0f;
                animator.Play("Idle");
                moving = false;
            }
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
