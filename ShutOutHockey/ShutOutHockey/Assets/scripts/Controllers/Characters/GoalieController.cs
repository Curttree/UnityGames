using UnityEngine;

public class GoalieController : MonoBehaviour {
    public Animator anim;
    public GameObject demoTargets;
    public GameObject sprayRight;
    public GameObject sprayLeft;
    public AudioClip[] clips;
    public bool delayedSave;

    private AudioSource audioSource;
    private bool soundEffectsOn = true;

    public void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        anim.SetBool("Intro", true);

        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            soundEffectsOn = PlayerPrefs.GetInt("SoundEffects") != 0;
        }
    }
    public void Save(int target)
    {
        delayedSave = false;
        anim.SetInteger("Target", target);
        switch (target)
        {
            case 1:
                GetComponent<Rigidbody2D>().MovePosition(new Vector3(0f, 0.5f, 0f));
                Instantiate(sprayRight, new Vector3(2.75f, -2.25f, 0), Quaternion.identity);
                break;
            case 2:
                GetComponent<Rigidbody2D>().MovePosition(new Vector3(0f, 0.5f, 0f));
                Instantiate(sprayLeft, new Vector3(-5f, -1.85f, 0), Quaternion.identity);
                break;
            case 3:
                GetComponent<Rigidbody2D>().MovePosition(new Vector3(1.1f, 0.5f, 0f));
                break;
            case 4:
                GetComponent<Rigidbody2D>().MovePosition(new Vector3(-1.1f, 0.5f, 0f));
                break;
            default:
                GetComponent<Rigidbody2D>().MovePosition(new Vector3(0f, 0.5f, 0f));
                break;
        }
        if (target > 0 && soundEffectsOn)
        {
            PlaySound(target);
        }
    }

    public void PlaySound(int target)
    {
        int selection = Random.Range(0, clips.Length);
        audioSource.clip = clips[selection];
        audioSource.Play();
    }

    public void IntroEnded()
    {
        anim.SetBool("Intro", false);
        Destroy(demoTargets);
    }

    public int GetCoveredTarget()
    {
        return anim.GetInteger("Target");
    }
}
