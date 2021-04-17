using System;
using UnityEngine;
using UnityEngine.UI;

public class LessonView : MonoBehaviour
{
    public Text stepText;
    public GameObject restartButton;
    public Text partNameText;
    public GameObject totalTimeText;

    private void OnEnable()
    {
        LessonApplication.Instance.controller.onStepUpdated += UpdateStepText;
        LessonApplication.Instance.controller.onStepUpdated += CheckForComplete;
        LessonApplication.Instance.controller.onLessonFailed += LessonFailed;
        SetRestartButtonActive(false);
        SetTimerTextActive(false);
        UpdateStepText(LessonApplication.Instance.model.GetCurrentStepDescription());
    }
    
    private void UpdateStepText(string text)
    {
        stepText.text = text;
    }

    private void Update()
    {
        if (LessonApplication.Instance.controller.LessonStopped())
        {
            return;
        }

        CheckForOutline();
        UserClick();
    }

    public void UserClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Step step = hit.transform.GetComponent<Step>();
                if (step != null)
                {
                    LessonApplication.Instance.controller.CheckStep(step);
                }
            }
        }
    }

    public void CheckForOutline()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            LessonApplication.Instance.controller.CheckOutlineAndPartName(hit.collider.gameObject);
        }
    }

    private void CheckForComplete(string text)
    {
        if (text == "Урок успешно завершен!")
        {
            SetRestartButtonActive(true);
            SetTimerTextActive(true);
        }
    }

    private void LessonFailed()
    {
        stepText.text = "Неправильный шаг! Урок провален.";
        SetRestartButtonActive(true);
    }

    private void SetRestartButtonActive(bool value) => restartButton.SetActive(value);

    private void SetTimerTextActive(bool value)
    {
        totalTimeText.transform.GetChild(0).GetComponent<Text>().text =
            $"{LessonApplication.Instance.controller.time:0.00} sec";
        totalTimeText.SetActive(value);
    }

    private void OnDisable()
    {
        LessonApplication.Instance.controller.onStepUpdated -= UpdateStepText;
        LessonApplication.Instance.controller.onStepUpdated -= CheckForComplete;
        LessonApplication.Instance.controller.onLessonFailed -= LessonFailed;
    }
}