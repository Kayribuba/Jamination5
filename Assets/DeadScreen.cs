using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadScreen : MonoBehaviour
{
    public bool isDead = false;

    public GameObject deadMenu;
    public GameObject player;
    public GameObject hs;   
    public GameObject X5;   

    private int mainmenu_scene;

    private void Start()
    {
        deadMenu.SetActive(false);
        hs.SetActive(false);

        mainmenu_scene = SceneManager.GetActiveScene().buildIndex - 1;
    }

    void Update()
    {
        if (isDead)
        {
            Dead();
        }
    }


    private void Dead()
    {
        isDead = true;

        deadMenu.SetActive(true);
        hs.SetActive(true);
        player.SetActive(false);
        X5.SetActive(false);
    }

    public void Restart()
    {

        deadMenu.SetActive(false);
        hs.SetActive(false);
        player.SetActive(true);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainmenu_scene);
    }
}
