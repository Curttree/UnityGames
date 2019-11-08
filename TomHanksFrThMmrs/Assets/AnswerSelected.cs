using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerSelected : MonoBehaviour
{
    public bool correct;
    public ChoiceTree[] nextChoices;
    ChangeCursor cam;
    public AudioSource audioQueue;
    public bool playAudio;
    public GameObject spawn;

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
            }
            cam.activeChoice = false;
            cam.MoveNextChoice();
        }
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
    }
}
