using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    float timer, refresh, avgFramerate;
    string display = "{0} FPS";

    [SerializeField]
    TMP_Text text;
    
    void Update()
    {
        float timelapse = Time.smoothDeltaTime;
        timer = timer <= 0 ? refresh : timer -= timelapse;

        if(timer <= 0)
        {
            avgFramerate = (int)(1f / timelapse);
            text.text = string.Format(display, avgFramerate.ToString());
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }

}
