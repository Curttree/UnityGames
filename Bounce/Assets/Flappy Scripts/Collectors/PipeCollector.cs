using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PipeCollector : MonoBehaviour
{

    private GameObject[] pipeHolders;

    private float distance = 9f;
    private float lastPipesX;
    private float pipeMin = -1.1f;
    private float pipeMax = 2.9f;
    
    void Awake()
    {
        pipeHolders = GameObject.FindGameObjectsWithTag("PipeHolder");

        for (var x = 0; x < pipeHolders.Length; x++)
        {
            var temp = pipeHolders[x].transform.position;
            temp.y = Random.Range(pipeMin, pipeMax);
            pipeHolders[x].transform.position = temp;
            resetGrass(pipeHolders[x]);
        }

        lastPipesX = pipeHolders.Max(x => x.transform.position.x);
    }
    
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "PipeHolder")
        {
            Vector3 temp = target.transform.position;

            temp.x = lastPipesX + distance;
            temp.y = Random.Range(pipeMin, pipeMax);

            target.transform.position = temp;

            resetGrass(target.gameObject);

            lastPipesX = temp.x;
        }

    }

    void resetGrass(GameObject pipeHolder)
    {
        var grass = pipeHolder.GetComponentInChildren<DecorationController>()?.gameObject;
        if (grass != null)
        {
            grass.GetComponent<DecorationController>().RandomShow();
        }
    }
}
