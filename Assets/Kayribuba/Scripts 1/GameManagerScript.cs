using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI EditText;
    [SerializeField] bool editorModeIsOn;
    [SerializeField] GameObject Magenta;

    [SerializeField] GameObject platform1;

    [Header("AT LEAST 2 COURSES ARE NEEDED")]
    public GameObject[] Courses;

    GameObject lastCourse, currentCourse, nextCourse;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
        Vector3 spawnPos = cam.transform.position;
        spawnPos.z = 0;
        currentCourse = Instantiate(Courses[0], spawnPos, Quaternion.identity);
        lastCourse = Instantiate(Courses[0], spawnPos, Quaternion.identity);
    }
    void Update()
    {
        EditorMode();

        if (Camera.main.transform.position.x >= currentCourse.transform.position.x)
            SpawnNextCourses();
    }

    void SpawnNextCourses()
    {
        Destroy(lastCourse.gameObject);
        lastCourse = currentCourse;

        Vector3 spawnPoint = cam.transform.position;
        spawnPoint.x += 70;
        currentCourse = Instantiate(Courses[0], spawnPoint, Quaternion.identity);

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
            ReloadLevel();
        }
        if(Input.GetKeyDown(KeyCode.T) && editorModeIsOn)
        {
            FindObjectOfType<TurnManager>().ChangeType();
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
