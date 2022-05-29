using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject credits;  

    private int play_scene;

    private void Start()
    {
        credits.SetActive(false);
        play_scene = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void Play()
    {
        SceneManager.LoadScene(play_scene);
    }

    public void Credits()
    {
        credits.SetActive(true);
        menu.SetActive(false);
    }

    public void Back()
    {
        credits.SetActive(false);
        menu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();     
    }
}
