using UnityEngine;
using UnityEngine.UI;

public class Measurement
{
    public float UpperBound;

    public float LowerBound;

    public Text Label;

    public float Value;

    public bool Incrementing = true;

    public string labelText;

    public Text label;
    
    public void SetLabel()
    {
        label = GameObject.FindGameObjectWithTag(labelText).GetComponent<Text>();
    }
}
