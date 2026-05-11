using UnityEngine;

public class CrowdController : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip[] clips;
    private float fall = 0.05f;
    private float maxScale = 4f;
    private float minHeight;
    private float startHeight;
    private bool goingDown;
    public float scale = 0f;
    private GameObject gameController;
    private bool soundEffectsOn = true;


    // Use this for initialization
    void Start () {
        startHeight = transform.position.y;
        minHeight = startHeight - fall;
        gameController = GameObject.FindGameObjectWithTag("GameController");
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            soundEffectsOn = PlayerPrefs.GetInt("SoundEffects") != 0;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (scale > 0)
        {
            if (goingDown)
                transform.Translate(0, -(Time.deltaTime / scale), 0);
            else
                transform.Translate(0, (Time.deltaTime / scale), 0);

            if (transform.position.y <= minHeight && goingDown)
                goingDown = false;
            if (transform.position.y >= startHeight && !goingDown)
                goingDown = true;
        }
	}

    public float GetExcitement()
    {
        float saveStreak = gameController.GetComponent<OffenceController>().saveStreak;
        float excitement = 5 * maxScale - (saveStreak / 10f * maxScale);
        float returnVal = (excitement < maxScale) ? maxScale : excitement;
        return returnVal;
    }
    public void PlayCrowdAudio(int goals)
    {
        if (soundEffectsOn)
        {
            int index = 0;
            if (goals >= 5)
            {
                index = 2;
            }
            else if (goals >=1)
            {
                index = 1;
            }    
            audioSource.PlayOneShot(clips[index]);
        }
    }
}
