using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnClouds : MonoBehaviour
{
    public GameObject[] clouds;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCloud());
    }


    IEnumerator SpawnCloud()
    {
        var randomNum=0;
        while(true)
        {
            randomNum = Random.Range(0, clouds.Length);
            Instantiate(clouds[randomNum],transform);
            yield return new WaitForSeconds(10f);
        }

    }

}
