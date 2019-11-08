using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceNode : MonoBehaviour
{
    public string choice;
    public ChoiceTree[] nextOptions;
    public AudioSource audioQueue;
    public bool playAudio;
    public GameObject spawn;
}
