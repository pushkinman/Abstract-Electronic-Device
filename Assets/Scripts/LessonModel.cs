using System;
using System.Collections.Generic;

public class LessonModel
{
    public List<string> steps;
    public int stepNumber = 0;
    public bool IsFinished => stepNumber == steps.Count;
    public bool isFailed = false;

    public LessonModel(List<string> steps)
    {
        this.steps = steps;
    }

    public string GetCurrentStepDescription()
    {
        if (IsFinished)
        {
            return "Урок успешно завершен!";
        }

        return steps[stepNumber];
    }
}