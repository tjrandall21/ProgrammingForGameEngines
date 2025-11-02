using UnityEngine;
using UnityEngine.UI;

public class ButtonSwitch : MonoBehaviour
{
    [SerializeField]
    private Sprite unpressed = null;
    [SerializeField]
    private Sprite pressed = null;

    private SpriteRenderer spriteRenderer = null;

    private int objectsOnButton = 0;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject != null)
        {
            if (collision.gameObject.tag == "Crate" || collision.gameObject.tag == "Player")
            {
                if (objectsOnButton == 0)
                {
                    spriteRenderer.sprite = pressed;
                }
                objectsOnButton += 1;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject != null)
        {
            if (collision.gameObject.tag == "Crate" || collision.gameObject.tag == "Player")
            {
                if(objectsOnButton == 1)
                {
                    spriteRenderer.sprite = unpressed;
                }
                objectsOnButton -= 1;
            }
        }
    }
    bool IsPressed()
    {
        if (objectsOnButton > 0)
        {
            return true;
        }
        return false;
    }


}
