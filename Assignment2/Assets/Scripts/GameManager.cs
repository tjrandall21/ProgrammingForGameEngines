using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController player1;
    [SerializeField]
    private PlayerController player2;
    [SerializeField]
    private TextMeshProUGUI infoText;
    [SerializeField]
    private TextMeshProUGUI winText;
    private Vector3 respawnLocation;
    [SerializeField]
    private float playerRespawnOffset = 1f;

    string controlsText = "Player 1: WASD\nJumps high but can't press buttons\nPlayer 2: Arrow Keys\nShort jump but can press buttons";
    string defaultText = "Press Enter to view controls.";
    string winInfoText = "Press Enter to play again.\nPress Esc to quit.";

    bool controlsShowing = false;
    bool gameWon = false;

    private PlayerInput playerInput = null;
    private InputAction continueAction = null;
    private InputAction quitAction = null;

    private static GameManager instance = null;
    public static GameManager Instance { get { return instance; } }
    void Awake()
    {
        playerInput = new PlayerInput();
        continueAction = playerInput.UI.Continue;
        quitAction = playerInput.UI.Quit;
        respawnLocation = player1.transform.position;
        if (instance == null)
        {
            instance = this;
        }
        continueAction.performed += OnContinue;
        quitAction.performed += OnQuit;
    }

    private void OnEnable()
    {
        playerInput.Enable();
        continueAction.Enable();
        quitAction.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
        continueAction.Disable();
        quitAction.Disable();
    }

    public void SetRespawnPos(Vector3 pos)
    {
        respawnLocation = pos;
    }

    public void Respawn()
    {
        Vector3 player1Pos = respawnLocation;
        Vector3 player2Pos = respawnLocation;
        player1Pos.x -= playerRespawnOffset;
        player2Pos.x += playerRespawnOffset;

        player1.transform.position = player1Pos;
        player2.transform.position = player2Pos;
    }

    public void Win()
    {
        gameWon = true;
        winText.enabled = true;
        infoText.text = winInfoText;
    }

    void OnContinue(InputAction.CallbackContext context)
    {
        if (gameWon)
        {
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            if (controlsShowing)
            {
                infoText.text = defaultText;
                controlsShowing = false;
            }
            else
            {
                infoText.text = controlsText;
                controlsShowing = true;
            }
        }
    }

    void OnQuit(InputAction.CallbackContext context)
    {
        Application.Quit(); //pretty sure this doesnt work in the editor
    }
}
