using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Button : MonoBehaviour
{
    public GameObject panel;
    bool active;
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ButtonHelp()
    {
        panel.SetActive(true);
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }
    public void ButtonBack()
    {
        panel.SetActive(false);
    }
}
