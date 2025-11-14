using System.Threading;
using UnityEngine;

public class BarrierTrigger : MonoBehaviour
{
    [SerializeField] Collider[] colliders;

    [SerializeField] Renderer[] renderers;
    bool entered = false;

    void OnTriggerEnter(Collider other)
    {
        if (!entered && other.tag == "Player")
        {
            entered = true;
            foreach (Collider collider in colliders)
            {
                collider.enabled = !collider.enabled;
            }
            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = !renderer.enabled;
            }
        }
    }
}
