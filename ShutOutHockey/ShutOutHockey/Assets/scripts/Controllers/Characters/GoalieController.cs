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
    }

    public void IntroEnded()
    {
        anim.SetBool("Intro", false);
        Destroy(demoTargets);
}
}
