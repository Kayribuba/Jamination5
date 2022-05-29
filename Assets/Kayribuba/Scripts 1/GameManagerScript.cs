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

    void Start()
    {
        currentCourse = platform1;
        nextCourse = Courses[0];
    }
    void Update()
    {
        EditorMode();

        if (Camera.main.transform.position.x >= currentCourse.transform.Find("MiddlePoint").position.x && Camera.main.transform.position.x != 0)
            SpawnNextCourses();
    }

    void SpawnNextCourses()
    {
        Vector3 lastPointPOS, middlePointPOS, firstPointPOS;

        if (lastCourse != null)
            Destroy(lastCourse);

        lastCourse = currentCourse;
        currentCourse = nextCourse;

        if (Courses.Length == 0)
        {
            Debug.Log("No course is available");
            return;
        }

        nextCourse = Courses[Mathf.RoundToInt(Random.Range(0, Courses.Length - 0.9f))];
        Vector3 NextCoursePos = currentCourse.transform.Find("LastPoint").position;
        NextCoursePos.x += (nextCourse.transform.Find("MiddlePoint").transform.position.x - nextCourse.transform.Find("FirstPoint").transform.position.x);

        Instantiate(nextCourse, NextCoursePos, Quaternion.identity);
        Debug.Log("spawnla");
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
