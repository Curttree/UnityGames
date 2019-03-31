using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceTree : MonoBehaviour
{
    [TextArea(2, 5)]
    public string prompt;
    public bool solved;
    public ChoiceNode choiceA;
    public ChoiceNode choiceB;
    public ChoiceNode choiceC;
    public ChoiceNode choiceD;

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