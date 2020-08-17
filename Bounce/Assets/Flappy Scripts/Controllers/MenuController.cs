using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    [SerializeField]
    private GameObject[] birds;

    private bool isGreenBirdUnlocked, isRedBirdUnlocked;

    // Start is called before the first frame update
    void Start()
    {
        birds[GameController.instance.GetSelectedBird()].SetActive(true);
        CheckIfUnlocked();
    }
    
    public void PlayGame()
    {
        SceneFader.instance.FadeIn("gameplay");
    }

    void CheckIfUnlocked()
    {
        if (GameController.instance.IsGreenBirdUnlocked())
        {
            isGreenBirdUnlocked = true;
        }
        if (GameController.instance.IsRedBirdUnlocked())
        {
            isRedBirdUnlocked = true;
        }
    }

    public void ChangeBird()
    {
        if (GameController.instance.GetSelectedBird() == 0)
        {
            if (isGreenBirdUnlocked)
            {
                birds[0].SetActive(false);
                GameController.instance.SetSelectedBird(1);
                birds[GameController.instance.GetSelectedBird()].SetActive(true);
            }
        }
        else if (GameController.instance.GetSelectedBird() == 1)
        {
            if (isRedBirdUnlocked)
            {
                GameController.instance.SetSelectedBird(2);
            }
            else
            {
                GameController.instance.SetSelectedBird(0);
            }

            birds[1].SetActive(false);
            birds[GameController.instance.GetSelectedBird()].SetActive(true);
        }
        else if (GameController.instance.GetSelectedBird() == 2)
        {
            birds[2].SetActive(false);
            GameController.instance.SetSelectedBird(0);
            birds[GameController.instance.GetSelectedBird()].SetActive(true);

        }
    }
}
