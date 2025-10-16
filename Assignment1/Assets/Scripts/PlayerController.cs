using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject ballPrefab = null;
    [SerializeField]
    private float moveSpeed = 10.0f;
    [SerializeField]
    private float maxDistanceFromStart = 8f;
    [SerializeField]
    private float dropCooldown = 10.0f;
    private float timeToNextDrop = 0.0f;

    [SerializeField]
    private SpriteRenderer renderer;
    [SerializeField]
    private Sprite closedHand;
    [SerializeField]
    private Sprite openHand;

    private Vector3 startPosition;
    void Awake()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            position.x -= moveSpeed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            position.x += moveSpeed;
        }
        if (Input.GetKeyDown(KeyCode.Space) && timeToNextDrop <= 0)
        {
            Vector3 spawnPos = transform.position;
            Instantiate(ballPrefab, spawnPos, quaternion.identity);
            timeToNextDrop = dropCooldown;
            renderer.sprite = openHand;
        }
        if(timeToNextDrop > 0)
        {
            timeToNextDrop -= Time.deltaTime;
            if(timeToNextDrop<=0)
            {
                renderer.sprite = closedHand;
            }
        }
        position.x = Mathf.Clamp(position.x, startPosition.x - maxDistanceFromStart, startPosition.x + maxDistanceFromStart);
        transform.position = position;
    }
}
