using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerSelected : MonoBehaviour
{
    public bool correct;
    public ChoiceTree nextChoice;
    ChangeCursor cam;

    void Start()
    {
        cam = Camera.main.GetComponent<ChangeCursor>();
    }

    public bool Selected()
    {
        if (correct)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SelectedChoice()
    {
        if (nextChoice != null)
        {
            Debug.Log("add choice");
            cam.choices.Add(nextChoice);
            if (nextChoice.solved)
            {
                nextChoice.GiveObject();
            }
            cam.activeChoice = false;
            cam.MoveNextChoice();
        }

    }
}
