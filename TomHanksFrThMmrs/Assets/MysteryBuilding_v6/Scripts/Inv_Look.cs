using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inv_Look : MonoBehaviour, IPointerClickHandler
{

    ChangeCursor cam;
    [TextArea(2, 5)]
<<<<<<< HEAD
    public string[] lookMessages;
=======
    public string lookMessage;
>>>>>>> f1cf2f85df54b696c8f046b3bee6d6fcb970156b
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
<<<<<<< HEAD
            cam.EnableDialog(lookMessages,face,personName);
=======
            cam.EnableDialog(lookMessage,face,personName);
>>>>>>> f1cf2f85df54b696c8f046b3bee6d6fcb970156b
    }

    public void OnHover()
    {
           //Debug.Log("Hovering");

    }

    public void OnClick()
    {

        //Debug.Log("fdds");
<<<<<<< HEAD
       // cam.EnableDialog(lookMessage);
=======
        cam.EnableDialog(lookMessage);
>>>>>>> f1cf2f85df54b696c8f046b3bee6d6fcb970156b
    }

}
