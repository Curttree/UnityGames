using System.Collections;
using System.Collections.Generic;
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
    private GameObject pausePanel, scorePanel, bouncePanel, gameOverPanel, newUnlock, scoreLabel, bounceLabel, bounceHighlight, bounceRed;

    [SerializeField]
    private GameObject[] ballList;

    private int soccerScore = 10;
    private int basketScore = 20;
    private int beachScore = 40;

    private int cityTotalScore = 100;
    private int gymTotalScore = 250;
    private int nightTotalScore = 1000;

    private bool instructionsHidden = false;

    private Dictionary<int, Balls> unlockableBalls = new Dictionary<int, Balls>()
    {   { 10, Balls.Soccer },
        { 20, Balls.Basket },
        { 40, Balls.Beach },
        { 50, Balls.Tennis } };

    private Dictionary<int, Backgrounds> unlockableBGs = new Dictionary<int, Backgrounds>()
    {
        { 100, Backgrounds.City},
        { 250, Backgrounds.Gym},
        { 1000, Backgrounds.Night}
    };

    public bool isPaused;

    private void Start()
    {
        var currentBG = GameController.instance.GetSelectedBG();
        BackgroundController.instance.ExternalChooseBackground(currentBG);
        scorePanel.gameObject.SetActive(true);
        bouncePanel.gameObject.SetActive(true);
        bounceCount.gameObject.SetActive(true);
        var ballNum = GameController.instance.GetSelectedBall();
        ActivateSelectedBall(ballNum);
        Time.timeScale = 1f;
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
                if (InstructionsShowing())
                {
                    instructionsButton.gameObject.SetActive(false);
                    instructionsHidden = true;
                }
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
        if (instructionsHidden)
        {
            instructionsHidden = false;
            ShowInstructions();
        }
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void RestartGame()
    {
        SceneFader.instance.FadeIn(SceneManager.GetActiveScene().name);
    }

    public void HideLabels()
    {
        instructionsButton.gameObject.SetActive(false);
        scoreLabel.gameObject.SetActive(false);
        bounceLabel.gameObject.SetActive(false);
    }

    public bool InstructionsShowing()
    {
        return instructionsButton.gameObject.activeSelf == true;
    }

    public void ShowInstructions()
    {
        instructionsButton.gameObject.SetActive(true);
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
        GameController.instance.SetPrevScore(score);

        endScore.text = score.ToString();
        if (score > GameController.instance.GetHighScore())
        {
            GameController.instance.SetHighScore(score);
        }
        bestScore.text = GameController.instance.GetHighScore().ToString();
        var unlockChar = UnlockCharacters(score);
        var unlockLev = UnlockLevels(newLifeScore);

        if (unlockChar || unlockLev)
        {
            newUnlock.SetActive(true);
        }
        else
        {
            newUnlock.SetActive(false);
        }

        restartGameButton.onClick.RemoveAllListeners();
        restartGameButton.onClick.AddListener(() => RestartGame());
    }

    private bool UnlockCharacters(int score)
    {
        var result = false;
        //Basketball
        if (score > basketScore && !GameController.instance.IsBasketBallUnlocked())
        {
            result = true;
            GameController.instance.UnlockBasketBall();
        }
        
        //Beachball
        if (score > beachScore && !GameController.instance.IsBeachBallUnlocked())
        { 
            result = true;
            GameController.instance.UnlockBeachBall();
        }

        //Soccerball
        if (score > soccerScore && !GameController.instance.IsSoccerBallUnlocked())
        {
            result = true;
            GameController.instance.UnlockSoccerBall();
        }

        return result;
    }

    private bool UnlockLevels(int lifeScore)
    {
        var result = false;
        //City
        if (lifeScore > cityTotalScore && !GameController.instance.IsCityBGUnlocked())
        {
            result = true;
            GameController.instance.UnlockCityBG();
        }
        //Gym
        if (lifeScore > gymTotalScore && !GameController.instance.IsGymBGUnlocked())
        {
            result = true;
            GameController.instance.UnlockGymBG();
        }
        //Night
        if (lifeScore > nightTotalScore && !GameController.instance.IsNightBGUnlocked())
        {
            result = true;
            GameController.instance.UnlockNightBG();
        }
        return result;
    }

    private void ActivateSelectedBall(int ballNum)
    {
        if (ballNum == ballList.Length)
        {
            var randChoice = Random.Range(0, ballList.Length);
            ballList[randChoice].SetActive(true);
        }
        else
        {
            ballList[ballNum].SetActive(true);
        }
    }

    private void ActivateSelectedBG(int bgNum)
    {
        if (bgNum == ballList.Length)
        {
            var randChoice = Random.Range(0, ballList.Length);
            ballList[randChoice].SetActive(true);
        }
        else
        {
            ballList[bgNum].SetActive(true);
        }
    }
}
