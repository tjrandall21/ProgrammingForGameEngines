using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidbody;
    [SerializeField]
    private float moveSpeed = 10.0f;
    [SerializeField]
    private float maxDistanceFromStart = 8f;
    private Vector3 startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        rigidbody.linearVelocityX = 0;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody.linearVelocityX -= moveSpeed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody.linearVelocityX += moveSpeed;
        }
        position.x = Mathf.Clamp(position.x, startPosition.x - maxDistanceFromStart, startPosition.x + maxDistanceFromStart);
        transform.position = position;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.gameObject != null)
        {
            if (collision.gameObject.tag == "Mushroom")
            {
                Debug.Log("Mushroom Get Points");
            }
            Destroy(collision.gameObject);
        }
    }
}
