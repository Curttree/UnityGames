using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueTree : MonoBehaviour, IPointerClickHandler
{
    public ChoiceTree[] choices;
    ChangeCursor cam;
    [TextArea(2, 5)]
    public string lookMessage;
    public Sprite face;
    public string personName;
<<<<<<< HEAD
    [Header("Solved")]
    public bool solved;
    public ChoiceTree[] solvedChoices;
=======
>>>>>>> f1cf2f85df54b696c8f046b3bee6d6fcb970156b
    // Use this for initialization
    void Start()
    {
        cam = Camera.main.GetComponent<ChangeCursor>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
<<<<<<< HEAD
        if (!cam.inConversation)
        {
            if (!solved)
            {
                cam.EnableDialog(choices, face, personName);
            }
            else
            {
                cam.EnableDialog(solvedChoices, face, personName);
            }
        }
=======
        if(!cam.inConversation)
            cam.EnableDialog(choices, face, personName);
>>>>>>> f1cf2f85df54b696c8f046b3bee6d6fcb970156b
    }

    public void OnClick()
    {

        //Debug.Log("fdds");
        cam.EnableDialog(choices[0].prompt);
    }
}
