using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI scoreDisplay;

    private int score = 0;

    private static GameManager instance = null;
    public static GameManager Instance {get{ return instance; }}
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreDisplay.text = "Score: " + score;
    }
}
