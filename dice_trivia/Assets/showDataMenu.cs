using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class showDataMenu : MonoBehaviour
{

    public string MenuItem = "";
    public string MenuType = "";

    Text DATA;

    GameObject gameStats;

    void Start()
    {
        gameStats = GameObject.Find("statsManager");
        DATA = GetComponent<Text>();
    }


    void Update()
    {
        statsControl temp = gameStats.GetComponent<statsControl>();
        if (MenuItem == "Time")
        {
            DATA.text = temp.GetData(MenuType, "Time");
        }
        else if (MenuItem == "Moves")
        {
            DATA.text = temp.GetData(MenuType, "Moves");
        }
        else if (MenuItem == "Correct")
        {
            DATA.text = temp.GetData(MenuType, "Correct");
        }
        else if (MenuItem == "False")
        {
            DATA.text = temp.GetData(MenuType, "False");
        }
        else if (MenuItem == "IAR")
        {
            DATA.text = temp.GetData(MenuType, "IAR");
        }

    }
}
