using System;
using System.Collections;
using UnityEngine;

public class LessonController : MonoBehaviour
{
    public event Action<string> onStepUpdated;
    public event Action onLessonFailed;
    public float time = 0;
    private string _newObjectName;
    private string _oldObjectName;
    private Outline[] _outlines;

    private void Start()
    {
        GetOutlines();
        TurnOffOutlines();
        StartCoroutine(StartTimer());
    }

    private void GetOutlines()
    {
        _outlines = GameObject.FindObjectsOfType<Outline>();
    }

    private void TurnOffOutlines()
    {
        foreach (Outline outline in _outlines)
        {
            outline.enabled = false;
        }
    }

    IEnumerator StartTimer()
    {
        while (!LessonApplication.Instance.model.IsFinished)
        {
            time += Time.deltaTime;
            yield return null;
        }
    }

    public bool LessonStopped() =>
        LessonApplication.Instance.model.IsFinished || LessonApplication.Instance.model.isFailed;

    public void CheckOutlineAndPartName(GameObject obj)
    {
        _newObjectName = obj.name;
        var outline = obj.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = true;
            LessonApplication.Instance.view.partNameText.text = obj.GetComponent<Step>().PartName;
            if (_newObjectName != _oldObjectName)
            {
                var oldObject = GameObject.Find(_oldObjectName);
                if (oldObject != null)
                {
                    oldObject.GetComponent<Outline>().enabled = false;
                }

                Debug.Log(10);
                _oldObjectName = _newObjectName;
            }
        }
        else
        {
            TurnOffOutlines();
            LessonApplication.Instance.view.partNameText.text = "";
        }
    }

    public void CheckStep(Step step)
    {
        if (step.CurrentDescription == LessonApplication.Instance.model.GetCurrentStepDescription())
        {
            Debug.Log("Correct step!");
            LessonApplication.Instance.model.stepNumber++;
            onStepUpdated?.Invoke(LessonApplication.Instance.model.GetCurrentStepDescription());
            step.SetNextState();
        }
        else
        {
            Debug.Log("Wrong step");
            Debug.Log("Lesson Failed");
            LessonApplication.Instance.model.isFailed = true;
            TurnOffOutlines();
            onLessonFailed?.Invoke();
        }
    }
}