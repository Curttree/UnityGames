using UnityEngine;

public class MusicController : MonoBehaviour {
    private bool bgMusicOn = true;
    private bool soundEffectsOn = true;
    // Use this for initialization
    void Start () {
        if (PlayerPrefs.HasKey("BGMusic"))
        {
            bgMusicOn = PlayerPrefs.GetInt("BGMusic") != 0;
        }
        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            soundEffectsOn = PlayerPrefs.GetInt("SoundEffects") != 0;
        }
    }
	
    public void PlaySource(AudioSource source, AudioCategory category, float delay=0f)
    {
        if((category == AudioCategory.BGMusic && bgMusicOn) ||
           (category == AudioCategory.SoundEffect && soundEffectsOn))
        {
            source.Play();
        }
    }
}
