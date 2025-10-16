using UnityEngine;

public class DropItem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision!=null&&collision.gameObject!=null)
        {
            if (collision.gameObject.tag != "Player")
            {
            Destroy(gameObject);
            }
        }
    }
}
