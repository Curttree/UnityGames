using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{

    public static GameplayController instance;

    [SerializeField]
    private Text scoreText, endScore, bestScore, bounceCount, lifeScore;

    [SerializeField]
    private Button restartGameButton, instructionsButton;

    [SerializeField]
    private GameObject pausePanel,scorePanel,bouncePanel,gameOverPanel,scoreLabel,bounceLabel,bounceHighlight,bounceRed;

    [SerializeField]
    private GameObject[] birds;

    [SerializeField]
    private Sprite[] medals;

    [SerializeField]
    private Image medalImage;

    private int basketScore = 20;
    private int beachScore = 40;

    private int nightTotalScore = 100;

    public bool isPaused;

    private void Start()
    {
        var currentBG = GameController.instance.GetSelectedBG();
        BackgroundController.instance.SelectBackground(currentBG);
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PauseGame()
    {
        if (BallScript.instance != null)
        {
            if (BallScript.instance.isAlive)
            {
                isPaused = true;
                pausePanel.SetActive(true);
                gameOverPanel.SetActive(false);
                endScore.text = BallScript.instance.score.ToString();
                bestScore.text = GameController.instance.GetHighScore().ToString();
                IncrementLifeScore(BallScript.instance.score);
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
        var birdNum = GameController.instance.GetSelectedBall();
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

    private int IncrementLifeScore(int score)
    {
        var newLifeScore = score + GameController.instance.GetLifeScore();
        lifeScore.text = newLifeScore.ToString();
        return newLifeScore;
    }

    public void SetBounce(float bounce)
    {
        bounceCount.text = bounce.ToString("n2");
    }

    public void HighlightBounce()
    {
        StartCoroutine(FlashHighlight());
    }

    IEnumerator FlashHighlight()
    {
        bounceHighlight.SetActive(true);
        yield return StartCoroutine(CustomCoroutines.WaitForRealSeconds(.2f));
        bounceHighlight.SetActive(false);
    }

    public void NoBounce()
    {
        StartCoroutine(FlashRed());
    }

    IEnumerator FlashRed()
    {
        bounceRed.SetActive(true);
        yield return StartCoroutine(CustomCoroutines.WaitForRealSeconds(.2f));
        bounceRed.SetActive(false);
    }

    public void PlayerDiedShowScore(int score)
    {
        pausePanel.SetActive(true);
        gameOverPanel.SetActive(true);
        scorePanel.gameObject.SetActive(false);
        bouncePanel.gameObject.SetActive(false);
        var newLifeScore = IncrementLifeScore(score);
        GameController.instance.SetLifeScore(newLifeScore);

        endScore.text = score.ToString();
        if (score > GameController.instance.GetHighScore())
        {
            GameController.instance.SetHighScore(score);
        }
        bestScore.text = GameController.instance.GetHighScore().ToString();
        UnlockCharacters(score);
        UnlockLevels(newLifeScore);
        restartGameButton.onClick.RemoveAllListeners();
        restartGameButton.onClick.AddListener(() => RestartGame());
    }

    private void UnlockCharacters(int score)
    {
        //Basketball
        if (score > basketScore && !GameController.instance.IsBasketBallUnlocked())
        {
            GameController.instance.UnlockBasketBall();
        }
        
        //Beachball
        if (score > beachScore && !GameController.instance.IsBeachBallUnlocked())
        {
            GameController.instance.UnlockBeachBall();
        }
    }

    private void UnlockLevels(int lifeScore)
    {
        //Night
        if (lifeScore > nightTotalScore && !GameController.instance.IsNightBGUnlocked())
        {
            GameController.instance.UnlockNightBG();
        }
    }
}
