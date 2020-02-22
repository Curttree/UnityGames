using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int Health;
    public int DecrementAmount;
    public TextMeshProUGUI Output;

    public void UpdateDisplay()
    {
        Output.text = Health.ToString();
    }

    public void Decrement()
    {
        Health -= DecrementAmount;
        UpdateDisplay();
    }
}
