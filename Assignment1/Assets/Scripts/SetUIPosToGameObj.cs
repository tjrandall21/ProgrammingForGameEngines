using UnityEngine;

public class SetUIPosToGameObj : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private Camera camera;

    void Start()
    {
        camera = Camera.main;
        if (target != null && camera != null)
        {
            Vector3 screenPos = camera.WorldToScreenPoint(target.position);
            transform.position = screenPos;
        }
    }
}
