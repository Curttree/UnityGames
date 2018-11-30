using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public Slider difficultySlider;
    public Toggle soundeffectsToggle;
    public Toggle backgroundmusicToggle;
    public AudioSource bgSource;
    public GameObject settingsMenu;

    public void OnLoad()
    {
        settingsMenu.SetActive(true);
        if (PlayerPrefs.HasKey("Difficulty"))
        {
            difficultySlider.value = PlayerPrefs.GetFloat("Difficulty");
        }
        if (PlayerPrefs.HasKey("BGMusic"))
        {
            backgroundmusicToggle.isOn = PlayerPrefs.GetInt("BGMusic") != 0;
        }
        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            soundeffectsToggle.isOn = PlayerPrefs.GetInt("SoundEffects") != 0;
        }
    }
    public void OnSave()
    {
        //gets all preferences and calls UpdateSettings, CloseMenu
        float difficulty = GetDifficulty();
        bool bgmusic = GetBGMusic();
        bool sounds = GetSoundEffects();
        UpdateSettings(difficulty, bgmusic, sounds);
        CloseMenu();
    }

    void UpdateSettings(float difficulty, bool bgmusic, bool sounds)
    {
        //Updates the preferences
        PlayerPrefs.SetFloat("Difficulty", difficulty);
        PlayerPrefs.SetInt("BGMusic", bgmusic ? 1 : 0);
        PlayerPrefs.SetInt("SoundEffects", sounds ? 1 : 0);
    }

    void CloseMenu()
    {
        if (backgroundmusicToggle.isOn && !bgSource.isPlaying)
        {
            bgSource.UnPause();
        }
        else if (!backgroundmusicToggle.isOn && bgSource.isPlaying)
        {
            bgSource.Pause();
        }
        settingsMenu.SetActive(false);
    }

    float GetDifficulty()
    {
        //Returns the difficulty value set by the slider.
        return difficultySlider.value;
    }

    bool GetSoundEffects()
    {
        return soundeffectsToggle.isOn;
    }

    bool GetBGMusic()
    {
        return backgroundmusicToggle.isOn;
    }
}
