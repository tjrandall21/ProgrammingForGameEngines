using UnityEngine;

public class DebugPrint : MonoBehaviour
{
    [SerializeField]
    private bool firstUpdate = false;
    private bool firstFixedUpdate = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Start Called");
    }
    protected void Awake()
    {
        Debug.Log("Awake Called");
    }

    // Update is called once per frame
    void Update()
    {
        if (!firstUpdate)
        {
            Debug.Log("Update Called");
            firstUpdate = true;
        }
    }

    void FixedUpdate()
    {
        if (!firstFixedUpdate)
        {
            Debug.Log("Fixed Update Called");
            firstFixedUpdate = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEntered2D Called");
    }
}
