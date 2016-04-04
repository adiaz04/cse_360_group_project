using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour {

    public float torqueAmount = 100000;
    public float torque;
    public bool roll = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    if (roll == true)
        {
            Roll();
        }
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
