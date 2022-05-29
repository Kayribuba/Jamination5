using UnityEngine;

public class CharacterFollowR : MonoBehaviour
{
    [Range(0,10)]public float change = 0.5f;
    public CameraScroll cs;

    public void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.gameObject.CompareTag("Player"))
        {
            cs.speed += change;
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
