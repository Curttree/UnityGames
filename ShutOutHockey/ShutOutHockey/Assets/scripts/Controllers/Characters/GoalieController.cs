using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalieController : MonoBehaviour {
    public Sprite stand;
    public Sprite butterfly;
    public Sprite glove;
    public Sprite pad;

    public void Save(int target)
    {
        switch (target)
        {
            case 1:
                GetComponent<Rigidbody2D>().MovePosition(new Vector3(0.51f, -1f, 0f));
                GetComponent<SpriteRenderer>().sprite = glove;
                break;
            case 2:
                GetComponent<Rigidbody2D>().MovePosition(new Vector3(-0.41f, -1f, 0f));
                GetComponent<SpriteRenderer>().sprite = pad;
                break;
            case 3:
                GetComponent<Rigidbody2D>().MovePosition(new Vector3(1.51f, -1f, 0f));
                GetComponent<SpriteRenderer>().sprite = butterfly;
                break;
            case 4:
                GetComponent<Rigidbody2D>().MovePosition(new Vector3(-0.51f, -1f, 0f));
                GetComponent<SpriteRenderer>().sprite = butterfly;
                break;
            default:
                GetComponent<Rigidbody2D>().MovePosition(new Vector3(0.51f, -1f, 0f));
                GetComponent<SpriteRenderer>().sprite = stand;
                break;
        }
            

    }
}
