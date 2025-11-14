using System;
using System.Threading;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameWon = false;
    private float timeElapsed = 0.0f;
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] PlayerController player;

    void Update()
    {
        if (!gameWon)
        {
            timeElapsed += Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds((int)timeElapsed);
            timer.text = timeSpan.ToString(@"m\:ss");;
        }
        if (player.HasReachedGoal())
        {
            gameWon = true;
            winText.text = "YOU WON!!";
        }
    }

}
