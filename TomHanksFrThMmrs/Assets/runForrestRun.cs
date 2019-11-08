using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runForrestRun : MonoBehaviour
{
    public bool running;
    public Transform LeftTarget;
    public Transform RightTarget;
    public bool runRight = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (runRight && transform.position.x <= RightTarget.transform.position.x - 0.5)
        {
            var newPosition = Vector2.MoveTowards(transform.position, RightTarget.position, 5f * Time.deltaTime);
            transform.position = newPosition;
        }
        else if (runRight)
        {
            runRight = false;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        if (!runRight && transform.position.x >= LeftTarget.transform.position.x + 0.5)
        {
            var newPosition = Vector2.MoveTowards(transform.position, LeftTarget.position, 5f * Time.deltaTime);
            transform.position = newPosition;
        }
        else if (!runRight)
        {
            runRight = true;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }
}
