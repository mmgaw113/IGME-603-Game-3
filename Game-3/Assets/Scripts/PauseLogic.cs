using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseLogic : MonoBehaviour
{
    public bool paused = false;
    public GameObject canvas;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && !paused)
        {
            OnPause();
        }
    }
    void OnPause()
    {
        canvas.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }
    public void OnResume()
    {
        canvas.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }
    public void OnQuit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
