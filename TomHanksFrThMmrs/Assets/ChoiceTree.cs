using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceTree : MonoBehaviour
{
    [TextArea(2, 5)]
    public string prompt;
    public bool solved;
    public string choiceA;
    public string choiceB;
    public string choiceC;
    public string choiceD;
    public string correctMessage;
    public string incorrectMessage;
}