using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour {

    public float torqueAmount = 100000;
    public float torque;
    public bool roll = false;
    public bool value = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    if (roll == true)
        {
            Roll();
        }
        if (value)
        {
            CalculateValue();
        }

	}

    private int CalculateValue()
    {
        int value = 0;
        //float dotForward = Vector3.Dot(transform.forward, Vector3.up);
        float angle = Vector3.Angle(transform.up, Vector3.up);
        Debug.Log(angle);
        return value;
    }

    void Roll()
    {
        torque = Random.Range(500, torqueAmount) * (Time.deltaTime * 100);
        GetComponent<Rigidbody>().rotation = Random.rotation;
        GetComponent<Rigidbody>().AddTorque(transform.up * torque * torqueAmount);
        GetComponent<Rigidbody>().AddTorque(transform.right * torque * torqueAmount);
        roll = false;
    }
}
