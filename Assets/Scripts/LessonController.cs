using System;
using UnityEngine;

public class LessonController : MonoBehaviour
{
    public Action<string> onStepUpdated;
    public Action onLessonFailed;
    private string _newObjectName;
    private string _oldObjectName;

    private void Start()
    {
        TurnOffOutlines();
    }

    private void TurnOffOutlines()
    {
        var outlines = GameObject.FindObjectsOfType<Outline>();
        foreach (Outline outline in outlines)
        {
            outline.enabled = false;
        }
    }

    private void Update()
    {
        SetOutlineAndPartName();
        UserClick();
    }

    private void SetOutlineAndPartName()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            _newObjectName = hit.transform.name;
            var step = hit.transform.GetComponent<Outline>();
            if (step != null)
            {
                step.enabled = true;
                LessonApplication.Instance.view.partNameText.text = hit.transform.GetComponent<Step>().PartName;
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
    }

    public void UserClick()
    {
        if (LessonApplication.Instance.model.isFinished || LessonApplication.Instance.model.isFailed)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Step step = hit.transform.GetComponent<Step>();
                if (step != null)
                {
                    CheckStep(step);
                }
            }
        }
    }

    private void CheckStep(Step step)
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
            onLessonFailed?.Invoke();
        }
    }
}