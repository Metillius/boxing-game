using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public GameObject gameOverPanel;   
    public TextMeshProUGUI winnerText;
    public Button replayButton;        

    void Awake()
    {
        Instance = this;
        gameOverPanel.SetActive(false);
    }

    void Start()
    {
        replayButton.onClick.AddListener(Replay);
    }

    public void PlayerDied(string loserTag)
    {
        
        string winner = loserTag == "Player" ? "P2 WINS!" : "P1 WINS!";

        winnerText.text = winner;
        gameOverPanel.SetActive(true);

        Time.timeScale = 0f; 
    }

    void Replay()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}