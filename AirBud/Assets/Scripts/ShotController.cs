using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShotController : MonoBehaviour
{
    private int stage = 0;

    private float modifier = 0f;

    [SerializeField]
    private GameObject success, miss;

    [SerializeField]
    private Measurement power = new Measurement() { UpperBound = 1.0f, LowerBound = -1.0f, labelText="PowerLabel"};

    [SerializeField]
    private Measurement accuracy = new Measurement() { UpperBound = 1.0f, LowerBound = -1.0f, labelText = "AccuracyLabel" };

    [SerializeField]
    private Measurement touch = new Measurement() { UpperBound = 1.0f, LowerBound = 0.0f, labelText = "TouchLabel" };

    private void Start()
    {
        modifier = Time.deltaTime;
    }
    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        stage = 0;
        power.SetLabel();
        accuracy.SetLabel();
        touch.SetLabel();
        ResetUI();
    }

    // called first
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (power?.label is null)
        {
            print("ruhroh");
        }
        else
        {
            UpdateCurrentValue();
            UpdateUI();
        }
    }

    void ResetUI()
    {
        power.label.text = null;
        accuracy.label.text = null;
        touch.label.text = null;
    }

    void UpdateUI()
    {
        if (stage >= (int) Stages.Power)
        {
            power.label.text = power.Value.ToString("0.00");
        }

        if (stage >= (int)Stages.Accuracy)
        {
            accuracy.label.text = accuracy.Value.ToString("0.00");
        }

        if (stage == (int)Stages.Touch)
        {
            touch.label.text = touch.Value.ToString("0.00");
        }
    }

    void UpdateCurrentValue()
    {
        switch ((Stages)stage)
        {
            case (Stages.Power):
                Cycle(power);
                break;
            case (Stages.Accuracy):
                Cycle(accuracy);
                break;
            case (Stages.Touch):
                Cycle(touch);
                break;
            case (Stages.Shot):
                CalculateShot();
                break;
            default:
                //Should not enter this stage. Return without updating.
                break;
        }
    }

    void Cycle(Measurement measure)
    {
        if (measure.Incrementing && measure.Value <= measure.UpperBound)
        {
            measure.Value += modifier;
        }
        else if (measure.Incrementing && measure.Value > measure.UpperBound)
        {
            measure.Incrementing = false;
            measure.Value = measure.UpperBound;
        }
        else if (!measure.Incrementing && measure.Value >= measure.LowerBound)
        {
            measure.Value -= modifier;
        }
        else if (!measure.Incrementing && measure.Value < measure.LowerBound)
        {
            measure.Incrementing = true;
            measure.Value = measure.LowerBound;
        }
        else
        {
            //Should not enter. Log value for debug purposes.
            print(measure.Value);
        }
    }

    public void EnterShotValue()
    {
        stage++;
    }

    public void CalculateShot()
    {
        //TODO: Come up with more accurate shot physics.
        var powerFixed = Math.Abs(power.Value) / (touch.Value * 2f);
        var accFixed = Math.Abs(accuracy.Value) / (touch.Value * 2f);

        print($"Power:{powerFixed},Acc:{accFixed}");
        if (Math.Abs(powerFixed) + Math.Abs(accFixed) < 0.1f)
        {
            success.SetActive(true);
        }
        else {
            miss.SetActive(true);
        }
        stage++;
    }
}
