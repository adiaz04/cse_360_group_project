using UnityEngine;
using System.Collections;

public class avatar0 : MonoBehaviour {


    GameObject gameStats;
    int currentAvatar;

    // Use this for initialization
    void Start () {
        gameStats = GameObject.Find("statsManager");
        statsControl temp = gameStats.GetComponent<statsControl>();
        currentAvatar = temp.sendAvatar();
        if (currentAvatar == 0)
        {
            gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

}
