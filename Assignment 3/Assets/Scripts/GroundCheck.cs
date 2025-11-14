using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private int objectsUnderPlayer = 0;
    void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject != null)
        {
            if (other.gameObject.layer == 3 && other.enabled)
            {
                objectsUnderPlayer += 1;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other != null && other.gameObject != null)
        {
            if (other.gameObject.layer == 3 && other.enabled && objectsUnderPlayer > 0)
            {
                objectsUnderPlayer -= 1;
            }
        }
    }
    public bool IsGrounded()
    {
        return objectsUnderPlayer > 0;
    }
    public void OnJump()
    {
        objectsUnderPlayer = 0;
    }
}
