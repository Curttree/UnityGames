using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inv_Look : MonoBehaviour, IPointerClickHandler
{

    ChangeCursor cam;
    [TextArea(2, 5)]
    public string[] lookMessages;
    public Sprite face;
    public string personName;

    // Use this for initialization
    void Start () {
        cam = Camera.main.GetComponent<ChangeCursor>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("fdds");
        if(!cam.inConversation)
            cam.EnableDialog(lookMessages,face,personName);
    }

    public void OnHover()
    {
           //Debug.Log("Hovering");

    }

    public void OnClick()
    {

        //Debug.Log("fdds");
       // cam.EnableDialog(lookMessage);
    }

}
