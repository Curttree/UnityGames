using UnityEngine;

public class GoalieController : MonoBehaviour {
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
            case 3:
                GetComponent<Rigidbody2D>().MovePosition(new Vector3(1.71f, -1f, 0f));
                break;
            case 4:
                GetComponent<Rigidbody2D>().MovePosition(new Vector3(-1f, -1f, 0f));
                break;
            default:
                GetComponent<Rigidbody2D>().MovePosition(new Vector3(0f, -1f, 0f));
                break;
        }
    }

    public void IntroEnded()
    {
        anim.SetBool("Intro", false);
        Destroy(demoTargets);
}
}
