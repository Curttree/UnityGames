using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour
{
    public static BirdScript instance;

    [SerializeField]
    private Rigidbody2D myRigidBody;

    [SerializeField]
    private Animator anim;

    private float forwardSpeed = 3.6f;

    private float bounceSpeed = -20f;

    private float maxSpeed = 12f;

    private float maxBounces = 1f;

    private float bounceIncrease = 0.0042f;

    public bool isAlive;

    private bool isFlapping;

    private Button flapButton;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip flapClip, pointClip, deathClip;

    public int score = 0;

    [SerializeField]
    private GameObject hitSpark;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        isAlive = true;

        flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
        flapButton.onClick.AddListener(() => Flap());
        GameplayController.instance.SetBounce(maxBounces);
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            Vector3 temp = transform.position;
            temp.x += forwardSpeed * Time.deltaTime;
            transform.position = temp;
            if(myRigidBody.velocity.x<=forwardSpeed * Time.deltaTime)
            {
                temp.x = forwardSpeed * Time.deltaTime;
                temp.y = myRigidBody.velocity.y;
                myRigidBody.velocity = temp;
            }
            if (System.Math.Truncate(maxBounces + bounceIncrease) > System.Math.Truncate(maxBounces))
            {
                audioSource.PlayOneShot(pointClip);
            }
            maxBounces += bounceIncrease;
            GameplayController.instance.SetBounce(maxBounces);

            if (isFlapping)
            {
                isFlapping = false;
                myRigidBody.velocity = new Vector2(0, bounceSpeed);
                audioSource.PlayOneShot(flapClip);
                anim.SetTrigger("isFlapping");
            }

            if (myRigidBody.velocity.y >= maxSpeed)
            {
                myRigidBody.velocity = new Vector2(0, maxSpeed);
            }
           
            transform.Rotate(0, 0, -180 * Time.deltaTime);
        }
    }

    void SetCameraX()
    {
        CameraScript.offsetX = Camera.main.transform.position.x - transform.position.x - 1;
    }

    public void Flap()
    {
        if (maxBounces >= 1f)
        {
            maxBounces--;
            GameplayController.instance.SetBounce(maxBounces);
            isFlapping = true;
        }
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
        audioSource.PlayOneShot(deathClip);
        Instantiate(hitSpark, transform.position, Quaternion.identity);
        Debug.Log("test1");
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
}
