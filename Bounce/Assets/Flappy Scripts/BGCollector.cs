using System.Linq;
using UnityEngine;

public class BGCollector : MonoBehaviour
{

    private GameObject[] backgrounds;
    private GameObject[] grounds;

    private float lastBGX;
    private float lastGroundX;
    // Start is called before the first frame update
    void Awake()
    {
        backgrounds = GameObject.FindGameObjectsWithTag("Background");
        grounds = GameObject.FindGameObjectsWithTag("Ground");
        
        lastBGX = backgrounds.Max(x => x.transform.position.x);
        lastGroundX = backgrounds.Max(x => x.transform.position.x);
    }
    
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Background")
        {
            Vector3 temp = target.transform.position;
            float width = ((BoxCollider2D)target).size.x;

            temp.x = lastBGX + width;

            target.transform.position = temp;

            lastBGX = temp.x;
        }
        else if (target.tag == "Ground")
        {
            Vector3 temp = target.transform.position;
            float width = ((BoxCollider2D)target).size.x;

            temp.x = lastGroundX + width;

            target.transform.position = temp;

            lastGroundX = temp.x;
        }
    }
}
