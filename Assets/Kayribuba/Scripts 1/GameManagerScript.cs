using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public TextMeshProUGUI EditText;
    bool editorModeIsOn;

    void Update()
    {
        EditorMode();
    }
    void EditorMode()
    {
        if (Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.M) && !editorModeIsOn)
        {
            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            editorModeIsOn = true;
            EditText.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.R) && editorModeIsOn)
        {
            FindObjectOfType<GameManagerScript>().ReloadLevel();
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadLevel(int levelBuildIndex)
    {
        SceneManager.LoadScene(levelBuildIndex);
    }
}
