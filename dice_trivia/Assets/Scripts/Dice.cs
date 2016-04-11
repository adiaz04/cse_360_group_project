using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour {

    public float torqueAmount = 100000;
    public float torque;
    public int value = 0;

    public Transform startingPosition;
    private bool rolled = false;

    /// <summary>
    /// Returns the current status of the dice by checking velocity and angular velocity.
    /// </summary>
    private bool rolling
    {
        get
        {
            return !(GetComponent<Rigidbody>().velocity.sqrMagnitude < 0.1f) && !(GetComponent<Rigidbody>().angularVelocity.sqrMagnitude < 0.1f);
        }
    }

    /// <summary>
    /// Returns the value and resets the dice, preparing for the next roll.
    /// </summary>
    public int Value
    {
        get
        {
            return value;
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update () {
        if (!rolling && rolled)
            CalculateValue();
        if (!rolled)
        {
            transform.rotation = Random.rotation;
            transform.position = startingPosition.position;
        }
	}

    /// <summary>
    /// Reset dice back to it's starting location.
    /// </summary>
    private void resetLocation()
    {
        transform.position = startingPosition.position;
    }

    /// <summary>
    /// Calculate face up by taking the Dot Product of each vector.
    /// </summary>
    private void CalculateValue()
    {
        float dotForward = Vector3.Dot(transform.forward, Vector3.up);
        if (dotForward > 0.99f)
            value = 2;
        else if (dotForward < -0.99f)
            value = 1;

        float dotRight = Vector3.Dot(transform.right, Vector3.up);
        if (dotRight > 0.99f)
            value = 6;
        else if (dotRight < -0.99f)
            value = 5;

        float dotUp = Vector3.Dot(transform.up, Vector3.up);
        if (dotUp > 0.99f)
            value = 3;
        else if (dotUp < -0.99f)
            value = 4;
        Debug.Log(value);
    }

    /// <summary>
    /// Roll the dice.
    /// </summary>
    public void Roll()
    {
        GetComponent<Rigidbody>().useGravity = true;
        torque = Random.Range(500, torqueAmount) * (Time.deltaTime * 100);
        GetComponent<Rigidbody>().rotation = Random.rotation;
        GetComponent<Rigidbody>().AddTorque(transform.up * torque * torqueAmount);
        GetComponent<Rigidbody>().AddTorque(transform.right * torque * torqueAmount);
        rolled = true;
    }

    /// <summary>
    /// Reset Dice in prepartion for next roll.
    /// </summary>
    public void Reset()
    {
        GetComponent<Rigidbody>().useGravity = false;
        transform.position = startingPosition.position;
        rolled = false;
        value = 0;
    }
}
