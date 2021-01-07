using Assets.Scripts.Controllers;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;
    
    private int currentBG, currentBall;

    [SerializeField]
    private GameObject changeBall, changeBG, payButton, messagePanel;

    private bool anyBallsUnlocked = false, anyBGsUnlocked = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        currentBG = GameController.instance.GetSelectedBG();
        BackgroundController.instance.SelectBackground(currentBG);
        currentBall = GameController.instance.GetSelectedBall();
        BallsController.instance.SelectBall(currentBall);
        CheckIfUnlocked();
        CheckIfPaid();
        HideButtons();

    }
    
    public void PlayGame()
    {
        SceneFader.instance.FadeIn("gameplay");
    }

    void CheckIfUnlocked()
    {
        if (GameController.instance.IsBasketBallUnlocked() || GameController.instance.IsBeachBallUnlocked() || GameController.instance.IsSoccerBallUnlocked())
        {
            anyBallsUnlocked = true;
        }
        if (GameController.instance.IsNightBGUnlocked() || GameController.instance.IsCityBGUnlocked() || GameController.instance.IsGymBGUnlocked())
        {
            anyBGsUnlocked = true;
        }
    }

    void CheckIfPaid()
    {
        if (GameController.instance.IsPaidUser())
        {
            payButton.SetActive(false);
        }
    }
    
    void HideButtons()
    {
        if (!anyBallsUnlocked)
        {
            changeBall.SetActive(false);
        }
        if (!anyBGsUnlocked)
        {
            changeBG.SetActive(false);
        }
    }

    public void ChangeBall()
    {
        var newBall = BallsController.instance.CycleBalls();
        GameController.instance.SetSelectedBall(newBall);
    }

    public void ChangeBG()
    {
        var newBG = BackgroundController.instance.CycleBG();
        GameController.instance.SetSelectedBG(newBG);
    }

    public void ShowPanel()
    {
        messagePanel.SetActive(true);
    }

    public void HidePanel()
    {
        messagePanel.SetActive(false);
    }
}
