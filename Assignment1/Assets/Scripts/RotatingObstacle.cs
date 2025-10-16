using System.Data;
using Unity.Mathematics;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rigidbody;
    [SerializeField]
    float rotationSpeed = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.AddTorque(rotationSpeed * Time.deltaTime * 100);
    }
}
