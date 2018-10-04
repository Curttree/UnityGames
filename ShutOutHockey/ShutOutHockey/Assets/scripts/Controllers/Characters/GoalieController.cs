using UnityEngine;

public class GoalieController : MonoBehaviour {
    public Sprite stand;
    public Sprite butterfly;
    public Sprite glove;
    public Sprite pad;
    public Animator anim;
    public GameObject demoTargets;

    public void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Intro", true);
    }
    public void Save(int target)
    {
        anim.SetInteger("Target", target);
        switch (target)
        {
            case 1:
                GetComponent<Rigidbody2D>().MovePosition(new Vector3(0.41f, -1f, 0f));
                GetComponent<SpriteRenderer>().sprite = glove;
                break;
            case 2:
                GetComponent<Rigidbody2D>().MovePosition(new Vector3(-0.41f, -1f, 0f));
                break;
            case 3:
                GetComponent<Rigidbody2D>().MovePosition(new Vector3(1.71f, -1f, 0f));
                break;
            case 4:
                GetComponent<Rigidbody2D>().MovePosition(new Vector3(-1f, -1f, 0f));
                break;
            case 5:
                GetComponent<Rigidbody2D>().MovePosition(new Vector3(0.51f, -1f, 0f));
                break;
            default:
                GetComponent<Rigidbody2D>().MovePosition(new Vector3(0.51f, -1f, 0f));
                break;
        }
    }

    public void IntroEnded()
    {
        anim.SetBool("Intro", false);
        Destroy(demoTargets);
}
}
