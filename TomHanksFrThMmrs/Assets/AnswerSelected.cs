using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerSelected : MonoBehaviour
{
    public bool correct;
<<<<<<< HEAD
    public ChoiceTree[] nextChoices;
    ChangeCursor cam;
    public AudioSource audioQueue;
    public bool playAudio;
    public GameObject spawn;
=======
    public ChoiceTree nextChoice;
    ChangeCursor cam;
>>>>>>> f1cf2f85df54b696c8f046b3bee6d6fcb970156b

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
<<<<<<< HEAD
        if (nextChoices.Length > 0)
        {
            foreach (ChoiceTree ch in nextChoices)
            {
                cam.choices.Add(ch);
            }
            if (nextChoices[0].solved) 
            {
                nextChoices[0].gameObject.GetComponent<DialogueTree>().solved = true;
            }
            if (nextChoices[0].giveItem)
            {
                nextChoices[0].GiveObject();
=======
        if (nextChoice != null)
        {
            Debug.Log("add choice");
            cam.choices.Add(nextChoice);
            if (nextChoice.solved)
            {
                nextChoice.GiveObject();
>>>>>>> f1cf2f85df54b696c8f046b3bee6d6fcb970156b
            }
            cam.activeChoice = false;
            cam.MoveNextChoice();
        }
<<<<<<< HEAD
        if (audioQueue != null)
        {
            if (playAudio && !audioQueue.isPlaying)
            {
                audioQueue.Play();
            }
            else if (!playAudio)
            {
                audioQueue.Stop();
            }
        }
        if(spawn != null)
        {
            spawn.SetActive(true);
        }
=======

>>>>>>> f1cf2f85df54b696c8f046b3bee6d6fcb970156b
    }
}
