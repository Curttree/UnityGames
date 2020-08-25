using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationController : MonoBehaviour
{

    private int showOdds = 3;

    [SerializeField]
    private Sprite[] decorations;

    public void RandomShow()
    {
        var choice = Random.Range(0, showOdds) == 0;
        if (choice)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            var temp = gameObject.transform.position;
            temp.y = -4;
            gameObject.transform.position = temp;
            SelectImage();
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void SelectImage()
    {
        if (decorations.Length > 0)
        {
            var selection = Random.Range(0, decorations.Length);
            gameObject.GetComponent<SpriteRenderer>().sprite = decorations[selection];
        }
    }
}
