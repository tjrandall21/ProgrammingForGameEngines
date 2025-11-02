using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private bool isGrounded = false;
    private bool isOnPlayer = false;
    private int objectsUnderPlayer = 0;

    private PlayerController player = null;

    void Awake()
    {
        player = GetComponentInParent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject != null)
        {
            if (collision.gameObject.layer == 3)
            {
                if (objectsUnderPlayer == 0)
                {
                    isGrounded = true;
                }
                objectsUnderPlayer += 1;
                if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
                {
                    isOnPlayer = true;
                }
                player.SetGrounded(isGrounded,isOnPlayer,objectsUnderPlayer);
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject != null)
        {
            if (collision.gameObject.layer == 3)
            {
                if(objectsUnderPlayer == 1)
                {
                    isGrounded = false;
                }
                objectsUnderPlayer -= 1;
                if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
                {
                    isOnPlayer = false;
                }
                player.SetGrounded(isGrounded,isOnPlayer,objectsUnderPlayer);
            }
        }
    }
}
