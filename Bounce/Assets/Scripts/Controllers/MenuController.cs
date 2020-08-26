using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    [SerializeField]
    private GameObject[] birds;

    private bool isBasketballUnlocked, isBeachballUnlocked;

    private int currentBG;

    [SerializeField]
    private GameObject changeBall, changeBG;

    private bool anyBallsUnlocked = false, anyBGsUnlocked = false;

    // Start is called before the first frame update
    void Start()
    {
        birds[GameController.instance.GetSelectedBall()].SetActive(true);
        currentBG = GameController.instance.GetSelectedBG();
        BackgroundController.instance.SelectBackground(currentBG);
        CheckIfUnlocked();
        HideButtons();
    }
    
    public void PlayGame()
    {
        SceneFader.instance.FadeIn("gameplay");
    }

    void CheckIfUnlocked()
    {
        if (GameController.instance.IsBasketBallUnlocked())
        {
            anyBallsUnlocked = true;
            isBasketballUnlocked = true;
        }
        if (GameController.instance.IsBeachBallUnlocked())
        {
            anyBallsUnlocked = true;
            isBeachballUnlocked = true;
        }
        if (GameController.instance.IsNightBGUnlocked())
        {
            anyBGsUnlocked = true;
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

    public void ChangeBird()
    {
        if (GameController.instance.GetSelectedBall() == 0)
        {
            if (isBasketballUnlocked)
            {
                birds[0].SetActive(false);
                GameController.instance.SetSelectedBall(1);
                birds[GameController.instance.GetSelectedBall()].SetActive(true);
            }
        }
        else if (GameController.instance.GetSelectedBall() == 1)
        {
            if (isBeachballUnlocked)
            {
                GameController.instance.SetSelectedBall(2);
            }
            else
            {
                GameController.instance.SetSelectedBall(0);
            }

            birds[1].SetActive(false);
            birds[GameController.instance.GetSelectedBall()].SetActive(true);
        }
        else if (GameController.instance.GetSelectedBall() == 2)
        {
            birds[2].SetActive(false);
            GameController.instance.SetSelectedBall(0);
            birds[GameController.instance.GetSelectedBall()].SetActive(true);

        }
    }

    public void ChangeBG()
    {
        var newBG = BackgroundController.instance.CycleBG();
        GameController.instance.SetSelectedBG(newBG);
    }
}
