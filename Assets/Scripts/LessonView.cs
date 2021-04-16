using System;
using UnityEngine;
using UnityEngine.UI;

public class LessonView : MonoBehaviour
{
    public Text stepText;
    public GameObject restartButton;
    public Text partNameText;

    private void Start()
    {
        PrepareView();
    }

    private void PrepareView()
    {
        LessonApplication.Instance.controller.onStepUpdated += UpdateStepText;
        LessonApplication.Instance.controller.onStepUpdated += CheckForComplete;
        LessonApplication.Instance.controller.onLessonFailed += LessonFailed;
        SetRestartButtonActive(false);
        UpdateStepText(LessonApplication.Instance.model.GetCurrentStepDescription());
    }

    public void UpdateStepText(string text)
    {
        stepText.text = text;
    }

    public void CheckForComplete(string text)
    {
        if (text == "Complete!")
        {
            SetRestartButtonActive(true);
        }
    }

    public void LessonFailed()
    {
        LessonApplication.Instance.model.isFailed = true;
        stepText.text = "Wrong Step! Lesson failed.";
        SetRestartButtonActive(true);
    }

    public void SetRestartButtonActive(bool value) => restartButton.SetActive(value);
}