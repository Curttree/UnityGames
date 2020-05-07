using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCursor : MonoBehaviour {/** Mask for the raycast placement */
	public LayerMask walkMask;
	//public LayerMask lookMask;
	public bool dialog;
    public bool choicesToBeMade;
	//IAstarAI[] ais;

	/** Determines if the target position should be updated every frame or only on double-click */

	public Texture2D cursorWalk;
	public Texture2D cursorDefault;
	public Texture2D cursorLook;
	public Texture2D cursorInteract;
	CursorMode cursorMode = CursorMode.Auto;
    Vector2 hotSpot = Vector2.zero;
    string activeGuy;
    public GameObject diaBox;
    public Text text;
    public  bool canWalk = false;
    public GameObject faceIcon;
    public Sprite defaultFace;
    public GameObject namePlate;
    public List<string> dialogList;
    public List<ChoiceTree> choices;
    public GameObject choiceA;
    public GameObject choiceB;
    public GameObject choiceC;
    public GameObject choiceD;
    public bool inConversation = false;
    public bool ignoreClick = false;

    public bool activeChoice = false;

<<<<<<< HEAD
    [Header("Intro")]
    public bool playIntro;
    [TextArea(2, 5)]
    public string[] introMessages;
    public Sprite introFace;
    public string introName;



    public void Start () {

		useGUILayout = false;
        if (playIntro)
        {
            EnableDialog(introMessages, introFace, introName);
        }
=======

	public void Start () {

		useGUILayout = false;
>>>>>>> f1cf2f85df54b696c8f046b3bee6d6fcb970156b
	}

	public void OnGUI () {
//		if (onlyOnDoubleClick && cam != null && Event.current.type == EventType.MouseDown && Event.current.clickCount == 2) {
//			UpdateTargetPosition();
//		}
	}

	/** Update is called once per frame */
	void Update () {

        TheHits();
        TheClicks();
    }

    public void LookAt() {

    }

    public void WalkTo() {
    }

    public void TheClicks() {
       
        if (Input.GetMouseButtonDown(0) && !ignoreClick)
            
        {
            if (dialog)
            {
                if (dialogList.Count <= 1)
                {
                    dialogList = new List<string>();
                    DisableDialog();
                }
                else
                {
                    dialogList.RemoveAt(0);
                    text.text = dialogList[0];
                }
            }
            if (choicesToBeMade)
            {
                //Debug.Log($"{choices[0].prompt}");
                if (text.gameObject.activeSelf)
                {
                    text.gameObject.SetActive(false);
<<<<<<< HEAD
                    Map(choiceA, choices[0].choiceA);
                    Map(choiceB, choices[0].choiceB);
                    Map(choiceC, choices[0].choiceC);
                    Map(choiceD, choices[0].choiceD);
=======
                    if (!string.IsNullOrEmpty(choices[0].choiceA?.choice))
                    {
                        activeChoice = true;
                        choiceA.GetComponent<Text>().text = choices[0].choiceA?.choice;
                        choiceA.GetComponent<AnswerSelected>().nextChoice = choices[0].choiceA?.nextOption;
                        choiceA.SetActive(true);
                    }
                    if (!string.IsNullOrEmpty(choices[0].choiceB?.choice))
                    {
                        activeChoice = true;
                        choiceB.GetComponent<Text>().text = choices[0].choiceB?.choice;
                        choiceB.GetComponent<AnswerSelected>().nextChoice = choices[0].choiceB?.nextOption;
                        choiceB.SetActive(true);
                    }
                    if (!string.IsNullOrEmpty(choices[0].choiceC?.choice))
                    {
                        activeChoice = true;
                        choiceC.GetComponent<Text>().text = choices[0].choiceC?.choice;
                        choiceC.GetComponent<AnswerSelected>().nextChoice = choices[0].choiceC?.nextOption;
                        choiceC.SetActive(true);
                    }
                    if (!string.IsNullOrEmpty(choices[0].choiceD?.choice))
                    {
                        activeChoice = true;
                        choiceD.GetComponent<Text>().text = choices[0].choiceD?.choice;
                        choiceD.GetComponent<AnswerSelected>().nextChoice = choices[0].choiceD?.nextOption;
                        choiceD.SetActive(true);
                    }
>>>>>>> f1cf2f85df54b696c8f046b3bee6d6fcb970156b
                    if (activeChoice)
                    {
                        text.gameObject.SetActive(false);
                    }
                    else
                    {
                        MoveNextChoice();
                    }
                }
                else
                {
                    //Debug.Log("test");
                    //MoveNextChoice();
                }
            }
        }
    }
<<<<<<< HEAD

    private void Map(GameObject answer, ChoiceNode node)
    {
        if (!string.IsNullOrEmpty(node?.choice))
        {
            activeChoice = true;
            answer.GetComponent<Text>().text = node?.choice;
            answer.GetComponent<AnswerSelected>().nextChoices = node?.nextOptions;
            answer.GetComponent<AnswerSelected>().audioQueue = node?.audioQueue;
            answer.GetComponent<AnswerSelected>().playAudio = node.playAudio;
            answer.GetComponent<AnswerSelected>().spawn = node?.spawn;
            answer.SetActive(true);
        }
    }
=======
>>>>>>> f1cf2f85df54b696c8f046b3bee6d6fcb970156b
    public void MoveNextChoice()
    {
        choiceA.gameObject.SetActive(false);
        choiceB.gameObject.SetActive(false);
        choiceC.gameObject.SetActive(false);
        choiceD.gameObject.SetActive(false);
        text.gameObject.SetActive(true);
        Debug.Log(choices.Count);
        if (choices.Count <= 1)
        {
            choices = new List<ChoiceTree>();
            DisableDialog();
        }
        else
        {
            choices.RemoveAt(0);
            text.text = choices[0].prompt;
        }
    }
    public void TheHits()
    {
        //If the left mouse button is clicked.

        //Get the mouse position on the screen and send a raycast into the game world from that position.
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity);
       // Physics2D.Raycast(RayUp.origin, RayUp.direction);
        Debug.DrawRay(worldPoint, Vector3.zero, Color.cyan);
        //If something was hit, the RaycastHit2D.collider will not be null.


        if (hit.collider != null && !inConversation)
            {
    
                if (!dialog)
                {
                 if (hit.transform.gameObject.GetComponent<Inv_Look>()|| hit.transform.gameObject.GetComponent<DialogueTree>())
                {
                    canWalk = false;
                     Cursor.SetCursor(cursorLook, hotSpot, cursorMode);
                    //Debug.Log("looking");

                }

                else if (hit.transform.gameObject.GetComponent<Inv_Collectable>())
                {
                    canWalk = false;
                    if (!hit.transform.gameObject.GetComponent<Inv_Collectable>().seen)
                    { Cursor.SetCursor(cursorLook, hotSpot, cursorMode); }
                    else { Cursor.SetCursor(cursorInteract, hotSpot, cursorMode); }

                }
                else if ( hit.transform.gameObject.GetComponent<Inv_Needed>())
                {
                    canWalk = false;
                    if (!hit.transform.gameObject.GetComponent<Inv_Needed>().seen)
                    { Cursor.SetCursor(cursorLook, hotSpot, cursorMode); }
                    else { Cursor.SetCursor(cursorInteract, hotSpot, cursorMode); }
                }
                else if (walkMask == (walkMask | (1 << hit.transform.gameObject.layer)))



                {
                    Cursor.SetCursor(cursorWalk, hotSpot, cursorMode);
                    canWalk = true;



                }

                else
                {
                    canWalk = false;
                    Cursor.SetCursor(cursorDefault, hotSpot, cursorMode);
                   //Debug.Log("nothing to do");
                }


                    activeGuy = hit.collider.gameObject.name;
                }
                else
                {
                canWalk = false;
                Cursor.SetCursor(cursorDefault, hotSpot, cursorMode);
               // DisableDialog();
                }
            }
        else
        {
            Cursor.SetCursor(cursorDefault, hotSpot, cursorMode);
            canWalk = false;
        }

    }
    




    public void DisableDialog() {
        diaBox.SetActive(false);
        dialog = false;
        choicesToBeMade = false;
        inConversation = false;
        activeChoice = false;
    }

    public void EnableDialog(string str)
    {
        text.text = str;
        faceIcon.GetComponent<Image>().sprite = defaultFace;
        namePlate.GetComponent<Text>().text = "Tom Hanks";
        diaBox.SetActive(true);
        dialog = true;
     
    }

    public void EnableDialog(string str, Sprite face, string name)
    {
        text.text = str;
        faceIcon.GetComponent<Image>().sprite = face;
        namePlate.GetComponent<Text>().text = name;
        diaBox.SetActive(true);
        inConversation = true;
        dialog = true;

    }
    public void EnableDialog(string[] str, Sprite face, string name)
    {
        canWalk = false;
        faceIcon.GetComponent<Image>().sprite = face;
        namePlate.GetComponent<Text>().text = name;
        text.text = str[0];
        diaBox.SetActive(true);
        dialog = true;
        inConversation = true;
        dialogList = new List<string>(str);
    }
    public void EnableDialog(ChoiceTree[] choi, Sprite face, string name)
    {
        canWalk = false;
        faceIcon.GetComponent<Image>().sprite = face;
        namePlate.GetComponent<Text>().text = name;
        text.text = choi[0].prompt;
        diaBox.SetActive(true);
        choicesToBeMade = true;
        inConversation = true;
        choices = new List<ChoiceTree>(choi);
    }

    public void Interact() {
        Cursor.SetCursor(cursorInteract, hotSpot, cursorMode);
    }

    public void UseItemCursor(Texture2D cursor)
    {
        Cursor.SetCursor(cursor, hotSpot, cursorMode);
    }
}
