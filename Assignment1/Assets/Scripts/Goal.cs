using TMPro;
using UnityEngine;

public class Goal : MonoBehaviour
{

    [SerializeField]
    private int pointValue;
    [SerializeField]
    private TextMeshProUGUI pointDisplay;

    void Awake()
    {
        pointDisplay.text = pointValue.ToString();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.gameObject != null)
        {
            Destroy(collision.gameObject);
            GameManager.Instance.IncreaseScore(pointValue);
        }
    }
}
