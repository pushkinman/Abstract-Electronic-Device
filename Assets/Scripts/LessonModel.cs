using System;
using System.Collections.Generic;

[Serializable]
public class LessonModel
{
    public List<string> steps;
    public int stepNumber = 0;
    public bool isFinished = false;
    public bool isFailed = false;

    public LessonModel(List<string> steps)
    {
        this.steps = steps;
    }

    public string GetCurrentStepDescription()
    {
        if (stepNumber == steps.Count)
        {
            isFinished = true;
            return "Complete!";
        }

        return steps[stepNumber];
    }
}