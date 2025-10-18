using UnityEngine;

public class Collectible : MonoBehaviour
{

    [SerializeField]
    private int pointValue = 10;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.gameObject != null)
        {
            GameManager.Instance.IncreaseScore(pointValue);
            Destroy(gameObject);
        }
    }
}
