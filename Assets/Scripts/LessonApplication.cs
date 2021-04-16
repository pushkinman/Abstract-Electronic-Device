using System;
using UnityEngine;

public class LessonApplication : MonoBehaviour
{
    public static LessonApplication Instance;

    public LessonModel model;
    public LessonView view;
    public LessonController controller;

    private int LessonNumber
    {
        get { return PlayerPrefs.GetInt("LevelNumber"); }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        SetLesson(LessonNumber);
    }

    private void SetLesson(int lessonNumber) => model = new LessonModel(Lessons.lessons[lessonNumber]);
}