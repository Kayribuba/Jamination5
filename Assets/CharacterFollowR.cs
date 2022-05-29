using UnityEngine;

public class CharacterFollowR : MonoBehaviour
{
    [Range(0,10)]public float change = 0.5f;
    public CameraScroll cs;
    


    void Start()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.CompareTag("Player"))
        {
            

            cs.speed += change;
            Debug.Log(cs.speed);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cs.speed -= change;
        }
    }
}
