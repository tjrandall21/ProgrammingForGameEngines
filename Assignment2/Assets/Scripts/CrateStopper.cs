using UnityEngine;

public class CrateStopper : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject != null)
        {
            if (collision.gameObject.tag == "Crate")
            {
                collision.gameObject.GetComponent<Rigidbody2D>().linearVelocityX = 0;
            }
        }
    }
}
