using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    public static BallScript instance;

    [SerializeField]
    private Rigidbody2D myRigidBody;

    [SerializeField]
    private Animator anim;

    private float forwardSpeed = 3.6f;

    private float introSpeed = 2.4f;

    private float bounceSpeed = -20f;

    private float maxSpeed = 12f;

    private float maxBounces = 2f;

    private float bounceIncrease = 0.0039f;

    public bool isAlive, isIntro;

    private bool isFlapping, isFalling, isStopped;

    private Button flapButton;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip flapClip, pointClip, deathClip, noBounceClip;

    [SerializeField]
    private string flapClipPath,pointClipPath,deathClipPath,noBounceClipPath;

    public int score = 0;

    [SerializeField]
    private GameObject hitSpark;

    [SerializeField]
    private GameObject trailObject;

    private float generateTrailDelayDefault = 0.5f;
    private float generateTrailDelay = 0.05f;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        isAlive = true;
        isIntro = true;

        flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
        flapButton.onClick.AddListener(() => Flap());
        GameplayController.instance.SetBounce(maxBounces);
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            Vector3 temp = transform.position;
            if (isIntro)
            {
                if (transform.position.x <= 0)
                {
                    if (transform.position.x >= -1 && transform.position.x <= 0.5 && transform.position.y <= -3.48)
                    {
                        myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0);
                    }
                    else
                    {
                        temp.x += introSpeed * Time.deltaTime;
                        transform.position = temp;
                    }
                }
                else if (!isStopped)
                {
                    isStopped = true;
                    if (!GameplayController.instance.InstructionsShowing())
                    {
                        GameplayController.instance.ShowInstructions();
                    }
                } 
            }
            else
            {
                temp.x += forwardSpeed * Time.deltaTime;
                transform.position = temp;
                if (myRigidBody.velocity.x != forwardSpeed * Time.deltaTime)
                {
                    temp.x = forwardSpeed * Time.deltaTime;
                    temp.y = myRigidBody.velocity.y;
                    myRigidBody.velocity = temp;
                }
            }
            if (Math.Truncate(maxBounces + bounceIncrease) > Math.Truncate(maxBounces))
            {
                PlaySound(pointClip, pointClipPath, 0.25f);
                GameplayController.instance.HighlightBounce();
            }
            if (!isIntro)
            {
                maxBounces += bounceIncrease;
                GameplayController.instance.SetBounce(maxBounces);
            }

            if (isFlapping)
            {
                isFlapping = false;
                isFalling = true;
                generateTrailDelay = generateTrailDelayDefault;
                myRigidBody.velocity = new Vector2(0, bounceSpeed);
            }

            if (isFalling && isAlive && !isStopped)
            {
                GenerateTrail();
            }

            if (myRigidBody.velocity.y >= maxSpeed)
            {
                myRigidBody.velocity = new Vector2(0, maxSpeed);
            }

            if (!isStopped)
            {
                transform.Rotate(0, 0, -180 * Time.deltaTime);
            }
            else
            {
                myRigidBody.velocity = new Vector2(0, 0);
            }
        }
    }

    void SetCameraX()
    {
        CameraScript.offsetX = Camera.main.transform.position.x - transform.position.x - 1;
    }

    public void Flap()
    {
        if (!GameplayController.instance.isPaused && maxBounces >= 1f && isAlive)
        {
            maxBounces--;
            GameplayController.instance.SetBounce(maxBounces);
            isFlapping = true;
        }
        else if (!GameplayController.instance.isPaused && maxBounces < 1f && !audioSource.isPlaying && isAlive)
        {
            PlaySound(noBounceClip, noBounceClipPath,0.5f);
            GameplayController.instance.NoBounce();
        }
        if (isIntro)
        {
            GameplayController.instance.HideLabels();
            isIntro = false;
        }
        if (isStopped)
        {
            isStopped = false;
        }
    }

    private IEnumerator GetScreen()
    {
        yield return CustomCoroutines.WaitForRealSeconds(0.15f);
        Time.timeScale = 0f;
    }
    public float GetPositionX()
    {
        return transform.position.x;
    }
    public float GetPositionY()
    {
        return transform.position.y;
    }

    private void Death()
    {
        isAlive = false;
        anim.SetTrigger("isDead");
        PlaySound(deathClip, deathClipPath);
        Instantiate(hitSpark, transform.position, Quaternion.identity);
        Time.timeScale = 0.25f;

        StartCoroutine(SlowMoDeath());
    }

    private void ShowScore()
    {
        Time.timeScale = 1f;
        GameplayController.instance.PlayerDiedShowScore(score);
    }


    IEnumerator SlowMoDeath()
    {
        yield return StartCoroutine(CustomCoroutines.WaitForRealSeconds(2f));
        ShowScore();
    }

    private void Score()
    {
        score++;
        GameplayController.instance.SetScore(score);
    }

    private void OnCollisionExit2D(Collision2D target)
    {
        if (target.gameObject.tag == "Ground")
        {
            if (isAlive)
            {
                if (isFalling)
                {
                    PlaySound(flapClip, flapClipPath);
                    anim.SetTrigger("isBouncing");
                }
                else
                {
                    PlaySound(flapClip, flapClipPath, 0.25f);
                }
                    isFalling = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Pipe")
        {
            if (isAlive)
            {
                Death();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "PipeHolder")
        {
            Score();
        }
    }

    private void GenerateTrail()
    {
        Instantiate<GameObject>(trailObject,gameObject.transform.position,gameObject.transform.rotation);
    }

    private void PlaySound(AudioClip clip, string path = null, float volume = 1f)
    {
        ////TODO: Come up with cleaner way to split audio.
        //if (path != null && Application.platform == RuntimePlatform.Android && AudioManager.instance != null)
        //{
        //    AudioManager.instance.PlaySound(path, volume);
        //}
        //else
        //{
            audioSource.PlayOneShot(clip, volume);
        //}

    }
}
