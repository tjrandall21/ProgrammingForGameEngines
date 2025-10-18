using UnityEngine;

public class DeathZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.gameObject != null)
        {
            Destroy(collision.gameObject);
        }
    }
}
