using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCloud : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("CloudTarget").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= target.position.x+ 2)
        {
            Destroy(gameObject);
        }
        else
        {
            var newPosition = Vector3.MoveTowards(transform.position, target.position, 5f * Time.deltaTime);
            transform.position = newPosition;
        }
    }
}
