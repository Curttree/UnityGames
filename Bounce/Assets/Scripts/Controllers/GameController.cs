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

    private const string SOCCER_BALL = "Soccer Ball";
    #endregion unlockable balls

    #region backgrounds
    private const string BG_NIGHT = "Night Background";
    private const string BG_CITY = "City Background";
    private const string BG_GYM = "Gym Background";
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
            PlayerPrefs.SetInt(SOCCER_BALL, 0);
            PlayerPrefs.SetInt(BG_NIGHT, 0);
            PlayerPrefs.SetInt(BG_CITY, 0);
            PlayerPrefs.SetInt(BG_GYM, 0);
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

    public void UnlockSoccerBall()
    {
        PlayerPrefs.SetInt(SOCCER_BALL, 1);
    }

    public bool IsSoccerBallUnlocked()
    {
        return PlayerPrefs.GetInt(SOCCER_BALL) == 1;
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

    public void UnlockCityBG()
    {
        PlayerPrefs.SetInt(BG_CITY, 1);
    }

    public bool IsCityBGUnlocked()
    {
        return PlayerPrefs.GetInt(BG_CITY) == 1;
    }

    public void UnlockGymBG()
    {
        PlayerPrefs.SetInt(BG_GYM, 1);
    }

    public bool IsGymBGUnlocked()
    {
        return PlayerPrefs.GetInt(BG_GYM) == 1;
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
