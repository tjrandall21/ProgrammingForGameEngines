using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController = null;
    static private GameController instance = null;
    static public GameController Instance { get { return instance; } }


    private void Initialize()
    {
        
    }
}
