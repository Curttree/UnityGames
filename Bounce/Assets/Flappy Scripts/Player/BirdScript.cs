using UnityEngine;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour
{
    public static BirdScript instance;

    [SerializeField]
    private Rigidbody2D myRigidBody;

    [SerializeField]
    private Animator anim;

    private float forwardSpeed = 3f;

    private float bounceSpeed = 4f;

    public bool isAlive;

    private bool isFlapping;

    private Button flapButton;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip flapClip, pointClip, deathClip;

    public int score = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        isAlive = true;

        flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
        flapButton.onClick.AddListener(() => Flap());
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            Vector3 temp = transform.position;
            temp.x += forwardSpeed * Time.deltaTime;
            transform.position = temp;

            if (isFlapping)
            {
                isFlapping = false;
                myRigidBody.velocity = new Vector2(0, bounceSpeed);
                audioSource.PlayOneShot(flapClip);
                anim.SetTrigger("isFlapping");
            }

            if(myRigidBody.velocity.y >= 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                float angle = 0;
                angle = Mathf.Lerp(0, -90, -myRigidBody.velocity.y / 10);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }

    void SetCameraX()
    {
        CameraScript.offsetX = Camera.main.transform.position.x - transform.position.x - 1;
    }

    public void Flap()
    {
        isFlapping = true;
    }

    public float GetPositionX()
    {
        return transform.position.x;
    }

    private void Death()
    {
        isAlive = false;
        anim.SetTrigger("isDead");
        audioSource.PlayOneShot(deathClip);
        GameplayController.instance.PlayerDiedShowScore(score);
    }

    private void Score()
    {
        score++;
        audioSource.PlayOneShot(pointClip);
        GameplayController.instance.SetScore(score);
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Ground" || target.gameObject.tag == "Pipe")
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
