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

    private void Start()
    {
        deadMenu.SetActive(false);
        hs.SetActive(false);
    }

    void Update()
    {
        if (isDead)
        {
            Dead();
            player.SetActive(false);

        }
    }


    private void Dead()
    {
        isDead = true;
        Time.timeScale = 0.0f;
        deadMenu.SetActive(true);
        hs.SetActive(true);

        
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        deadMenu.SetActive(false);
        player.SetActive(true);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
