using UnityEngine;

public class SavingSystem : MonoBehaviour
{
    public void SaveSceneBuildIndex(int sceneNumber)
    {
        PlayerPrefs.SetInt("LevelNumber", sceneNumber);
    }
}