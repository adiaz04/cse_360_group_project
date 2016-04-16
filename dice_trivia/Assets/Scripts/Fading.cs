using UnityEngine;
using System.Collections;


public class Fading : MonoBehaviour
{

    public Texture2D fadeOutTexture;
    public float fadeSpeed = 0.25f;

    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;

    /// <summary>
    /// OnGUI-make the Unity GUI fade to black with a value of fadeSpeed
    /// </summary>
    void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }

    /// <summary>
    /// BeginFade-returns fadeSpeed and sets the fadeDir to whatever direction the person wants the screen fades
    /// </summary>
    /// <param name="direction"> the way in which the screen shall fade</param>
    /// <returns>fadeSpeed- the amount of acceleration it will take to fade the screen</returns>
    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return (fadeSpeed);
    }

    /// <summary>
    /// OnLevelWasLoaded-calls BeginFade and gives a -1 value to make screen fade in
    /// </summary>
    void OnLevelWasLoaded()
    {
        BeginFade(-1);
    }
}
