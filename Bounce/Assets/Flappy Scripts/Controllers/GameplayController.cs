using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{

    public static GameplayController instance;

    [SerializeField]
    private Text scoreText, endScore, bestScore, bounceCount;

    [SerializeField]
    private Button restartGameButton, instructionsButton;

    [SerializeField]
    private GameObject pausePanel,scorePanel,bouncePanel,gameOverPanel,scoreLabel,bounceLabel;

    [SerializeField]
    private GameObject[] birds;

    [SerializeField]
    private Sprite[] medals;

    [SerializeField]
    private Image medalImage;

    private int bronzeScore = 20;
    private int goldScore = 40;

    public bool isPaused;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PauseGame()
    {
        if (BirdScript.instance != null)
        {
            if (BirdScript.instance.isAlive)
            {
                isPaused = true;
                pausePanel.SetActive(true);
                gameOverPanel.SetActive(false);
                endScore.text = BirdScript.instance.score.ToString();
                bestScore.text = GameController.instance.GetHighScore().ToString();
                Time.timeScale = 0f;
                restartGameButton.onClick.RemoveAllListeners();
                restartGameButton.onClick.AddListener(() => ResumeGame());
            }
        }

    }

    public void GoToMenuButton()
    {
        if (SceneFader.instance == null)
        {
            SceneManager.LoadScene("menu");
        }
        else
        {
            SceneFader.instance.FadeIn("menu");
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void RestartGame()
    {
        SceneFader.instance.FadeIn(SceneManager.GetActiveScene().name);
    }

    public void PlayGame()
    {
        scorePanel.gameObject.SetActive(true);
        bouncePanel.gameObject.SetActive(true);
        bounceCount.gameObject.SetActive(true);
        var birdNum = GameController.instance.GetSelectedBird();
        birds[birdNum].SetActive(true);
        instructionsButton.gameObject.SetActive(false);
        scoreLabel.gameObject.SetActive(false);
        bounceLabel.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void SetBounce(float bounce)
    {
        bounceCount.text = bounce.ToString("n2");
    }

    public void PlayerDiedShowScore(int score)
    {
        pausePanel.SetActive(true);
        gameOverPanel.SetActive(true);
        scorePanel.gameObject.SetActive(false);
        bouncePanel.gameObject.SetActive(false);

        endScore.text = score.ToString();
        if (score > GameController.instance.GetHighScore())
        {
            GameController.instance.SetHighScore(score);
        }

        bestScore.text = GameController.instance.GetHighScore().ToString();
        AwardMedal(score);
        UnlockCharacters(score);
        restartGameButton.onClick.RemoveAllListeners();
        restartGameButton.onClick.AddListener(() => RestartGame());
    }
    
    private void AwardMedal(int score)
    {
        if (score <= bronzeScore)
        {
            medalImage.sprite = medals[0];
        }
        else if (score > bronzeScore && score < goldScore)
        {
            medalImage.sprite = medals[1];
        }
        else
        {
            medalImage.sprite = medals[2];
        }
    }

    private void UnlockCharacters(int score)
    {
        
        //Green Bird
        if (score > bronzeScore && !GameController.instance.IsGreenBirdUnlocked())
        {
            GameController.instance.UnlockGreenBird();
        }
        
        //Red Bird
        if (score > goldScore && !GameController.instance.IsRedBirdUnlocked())
        {
            GameController.instance.UnlockRedBird();
        }
    }
}
