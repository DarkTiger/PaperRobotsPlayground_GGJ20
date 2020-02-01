using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    bool isPaused;

    private void Start()
    {
        isPaused = false;
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            //attivare o disattivare la pausa
            ChangePauseStatus();
        }
    }

    public void ChangePauseStatus()
    {
        isPaused = !isPaused;
        UpdateGamePause();
    }

    void UpdateGamePause()
    {
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

}
