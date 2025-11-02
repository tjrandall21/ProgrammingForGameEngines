using UnityEngine;

public class DeathZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject != null)
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
            {
                GameManager.Instance.Respawn();
            }
            else if (collision.gameObject.tag == "Crate")
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
