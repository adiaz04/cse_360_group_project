using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class timer : MonoBehaviour
{

    public Text timerLabel; //Text label for time
    private float time;    //value created to keep track of game time

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;

        var minutes = time / 60;//Gets minutes from game time
        var seconds = time % 60;//Gets seconds from game time

        //update the label value
        timerLabel.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
