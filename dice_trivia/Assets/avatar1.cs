using UnityEngine;
using System.Collections;

public class avatar1 : MonoBehaviour {

    GameObject gameStats;
    int currentAvatar;

    // Use this for initialization
    void Start()
    {
        gameStats = GameObject.Find("statsManager");
        statsControl temp = gameStats.GetComponent<statsControl>();
        currentAvatar = temp.sendAvatar();
        if (currentAvatar == 0)
        {
            gameObject.SetActive(false);
        }
        else {
            gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
