using UnityEngine;

public class SprayController : MonoBehaviour
{
    public Animator anim;
    public float velocity;
    public bool goingRight;
    public AudioClip[] clips;
    private int choices = 3;
    private AudioSource audioSource;
    private bool soundEffectsOn = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            soundEffectsOn = PlayerPrefs.GetInt("SoundEffects") != 0;
        }

        SelectSpray();
        anim.SetBool("GoingRight", goingRight);
        if (!goingRight)
        {
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    private void Update()
    {
        transform.Translate(new Vector3(velocity, 0, 0) * Time.deltaTime);
    }

    void SelectSpray()
    {
        int sprayChoice = Random.Range(0, choices);
        anim.SetInteger("Spray", sprayChoice);

        if (sprayChoice < clips.Length && soundEffectsOn)
        {
            audioSource.clip = clips[sprayChoice];
            audioSource.Play();
        }
    }
}
