using UnityEngine;

public class Goal : MonoBehaviour
{

    [SerializeField]
    int pointValue;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.gameObject != null)
        {
            Destroy(collision.gameObject);
            Debug.Log("Add Points");
        }
    }
}
