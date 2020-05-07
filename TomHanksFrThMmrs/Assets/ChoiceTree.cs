using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceTree : MonoBehaviour
{
    [TextArea(2, 5)]
    public string prompt;
    public bool solved;
<<<<<<< HEAD
    public bool giveItem;
=======
>>>>>>> f1cf2f85df54b696c8f046b3bee6d6fcb970156b
    public ChoiceNode choiceA;
    public ChoiceNode choiceB;
    public ChoiceNode choiceC;
    public ChoiceNode choiceD;
<<<<<<< HEAD
    public ChoiceTree[] nextQuestion;
=======
>>>>>>> f1cf2f85df54b696c8f046b3bee6d6fcb970156b

    SimpleInv inv;

    public Inven objectToGive;

    private void Start()
    {
        inv = FindObjectOfType<SimpleInv>();
    }

    public void GiveObject()
    {
        if (inv.CheckToAdd())
        {
            inv.addItem(objectToGive);
            solved = false;
        }

    }
}