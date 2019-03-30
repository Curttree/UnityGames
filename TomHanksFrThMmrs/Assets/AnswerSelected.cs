using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerSelected : MonoBehaviour
{
    public bool correct;
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
    }
}
