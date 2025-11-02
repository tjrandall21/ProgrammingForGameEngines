using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    int playersInZone = 0;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject != null)
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
            {
                if (tag == "WinZone")
                {
                    playersInZone++;
                    if (playersInZone == 2)
                    {
                        GameManager.Instance.Win();
                    }
                }
                else
                {
                    GameManager.Instance.SetRespawnPos(transform.position);
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject != null && tag == "WinZone")
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
            {

                    playersInZone--;

            }
        }
    }
}
