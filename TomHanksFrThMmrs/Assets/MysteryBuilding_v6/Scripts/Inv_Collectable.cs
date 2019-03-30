using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inv_Collectable : MonoBehaviour, IPointerClickHandler
{
    [TextArea(2, 5)]
    public string lookMessage;
    [TextArea(2, 5)]
    public string touchMessage;

    ChangeCursor cam;
    //public Transform nearestNode;
    public bool nearEnough;
     Transform target;
    public bool seen = false;
    public Inven item;
    SimpleInv inv;
    public float xoffset;
    public bool approaching = false;


    // Use this for initialization
    void Start () {
        cam = Camera.main.GetComponent<ChangeCursor>();
        inv = FindObjectOfType<SimpleInv>();
        target = GameObject.FindWithTag("Target").transform;
    }
    void Update()
    {
        if (approaching && nearEnough)
        {
            Collect();
            approaching = false;
        }
    }
	
	// Update is called once per frame
	public void OnHover () {
        //   Debug.Log("Hovering");
        if (seen) {
            cam.Interact();
        }
	}

    public void OnClick()
    {

        //if (nearEnough)
        //{
        //    Debug.Log("Clicked");
        //    if (Input.GetMouseButtonDown(0))
        //        cam.EnableDialog(lookMessage);
        //    else if (Input.GetMouseButtonDown(1))
        //        cam.EnableDialog(touchMessage);
        //}
        //else
        //{
        //   // target.position = nearestNode.position;
        //    // Debug.Log("MoveTarget");
        //}
    }

    

    public void OnLeave()
    {
        Debug.Log("NotHovering");
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            nearEnough = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        nearEnough = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!cam.inConversation)
        {
            if (!seen)
            {
                cam.EnableDialog(lookMessage);
                seen = true;

            }
            else
            {
                if (nearEnough)
                {
                    Collect();
                }
                else
                {
                    if (target.position.x < transform.position.x) xoffset = -xoffset;

                    Vector3 newPos = new Vector3(transform.position.x + xoffset, transform.position.y, target.position.z);
                    target.position = newPos;
                    approaching = true;
                }

            }
        }
    }
    private void Collect()
    {
        if (inv.CheckToAdd())
        {
            cam.EnableDialog(touchMessage);

            inv.addItem(item);
            Destroy(gameObject);
        }
        else
        {
            cam.EnableDialog("I can't carry anymore items");
        }
    }
}
