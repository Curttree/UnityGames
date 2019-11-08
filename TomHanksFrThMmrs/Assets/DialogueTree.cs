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
    [Header("Solved")]
    public bool solved;
    public ChoiceTree[] solvedChoices;
    // Use this for initialization
    void Start()
    {
        cam = Camera.main.GetComponent<ChangeCursor>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
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
    }

    public void OnClick()
    {

        //Debug.Log("fdds");
        cam.EnableDialog(choices[0].prompt);
    }
}
