using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    private const string HIGH_SCORE = "High Score";

    private const string LIFE_SCORE = "Life Score";

    private const string SELECTED_BALL = "Selected Ball";

    private const string SELECTED_BG = "Selected BG";

    #region unlockable balls
    private const string BASKET_BALL = "Basket Ball";

    private const string BEACH_BALL = "Beach Ball";
    #endregion unlockable balls

    #region backgrounds
    private const string BG_NIGHT = "Night Background";
    #endregion backgrounds

    #region trails

    #endregion trails

    private void Awake()
    {
        MakeSingleton();
        IsTheGameStartedForTheFirstTime();
    }
    // Start is called before the first frame update
    void IsTheGameStartedForTheFirstTime()
    {
        if (!PlayerPrefs.HasKey("FirstTime"))
        {
            PlayerPrefs.SetInt(HIGH_SCORE, 0);
            PlayerPrefs.SetInt(LIFE_SCORE, 0);
            PlayerPrefs.SetInt(SELECTED_BALL, 0);
            PlayerPrefs.SetInt(SELECTED_BG, 0);
            PlayerPrefs.SetInt(BASKET_BALL, 0);
            PlayerPrefs.SetInt(BEACH_BALL, 0);
            PlayerPrefs.SetInt(BG_NIGHT, 0);
            PlayerPrefs.SetInt("FirstTime", 0);
        }
    }

    public void SetHighScore(int score)
    {
        PlayerPrefs.SetInt(HIGH_SCORE, score);
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE);
    }

    public void SetLifeScore(int score)
    {
        PlayerPrefs.SetInt(LIFE_SCORE, score);
    }

    public int GetLifeScore()
    {
        return PlayerPrefs.GetInt(LIFE_SCORE);
    }

    public void SetSelectedBall(int selectedBall)
    {
        PlayerPrefs.SetInt(SELECTED_BALL, selectedBall);
    }

    public int GetSelectedBall()
    {
        return PlayerPrefs.GetInt(SELECTED_BALL);
    }

    public void UnlockBasketBall()
    {
        PlayerPrefs.SetInt(BASKET_BALL, 1);
    }

    public bool IsBasketBallUnlocked()
    {
        return PlayerPrefs.GetInt(BASKET_BALL) == 1;
    }

    public void UnlockBeachBall()
    {
        PlayerPrefs.SetInt(BEACH_BALL, 1);
    }

    public bool IsBeachBallUnlocked()
    {
        return PlayerPrefs.GetInt(BEACH_BALL) == 1;
    }

    public void SetSelectedBG(int selectedBG)
    {
        PlayerPrefs.SetInt(SELECTED_BG, selectedBG);
    }

    public int GetSelectedBG()
    {
        return PlayerPrefs.GetInt(SELECTED_BG);
    }

    public void UnlockNightBG()
    {
        PlayerPrefs.SetInt(BG_NIGHT, 1);
    }

    public bool IsNightBGUnlocked()
    {
        return PlayerPrefs.GetInt(BG_NIGHT) == 1;
    }

    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
