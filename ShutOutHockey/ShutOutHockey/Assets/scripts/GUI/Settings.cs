using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public Slider difficultySlider;
    public GameObject settingsMenu;

    public void OnLoad()
    {
        settingsMenu.SetActive(true);
        if (PlayerPrefs.HasKey("Difficulty"))
        {
            difficultySlider.value = PlayerPrefs.GetFloat("Difficulty");
        }
    }
    public void OnSave()
    {
        //gets all preferences and calls UpdateSettings, CloseMenu
        float difficulty = GetDifficulty();
        UpdateSettings(difficulty);
        CloseMenu();
    }

    void UpdateSettings(float difficulty)
    {
        //Updates the preferences
        PlayerPrefs.SetFloat("Difficulty", difficulty);
    }

    void CloseMenu()
    {
        settingsMenu.SetActive(false);
    }

    float GetDifficulty()
    {
        //Returns the difficulty value set by the slider.
        return difficultySlider.value;
    }
}
