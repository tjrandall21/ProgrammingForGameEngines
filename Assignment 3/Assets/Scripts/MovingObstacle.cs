using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

public class MovingObstacle : MonoBehaviour
{

    private Vector3 startPosition;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Vector3 relativeTargetPosition;
    private bool returning = false;
    private Vector3 moveDirection;
    private float distanceToTarget;
    private float distanceFromStart = 0;


    void Awake()
    {
        startPosition = transform.position;
        moveDirection = relativeTargetPosition;
        moveDirection.Normalize();
        distanceToTarget = relativeTargetPosition.magnitude;
    }


    void Update()
    {
        Vector3 currentRelativePosition = math.abs(transform.position - startPosition);
        Vector3 absRelativeTargetPosition = math.abs(relativeTargetPosition);
        if (returning)
        {
            distanceFromStart -= moveSpeed * Time.deltaTime;
            if (distanceFromStart <= 0)
            {

                returning = false;
            }
        }
        else
        {
            distanceFromStart += moveSpeed * Time.deltaTime;
            if (distanceFromStart >= distanceToTarget)
            {
                returning = true;
            }
        }
        math.clamp(distanceFromStart, 0, distanceToTarget);
        Vector3 currentPosition = startPosition + moveDirection * distanceFromStart;
        transform.position = currentPosition;
    }
}
