using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Step : MonoBehaviour
{
    public string PartName;
    
    private string _currentDescription;
    private Animator _animator => GetComponent<Animator>();  //To much?
    public string CurrentDescription
    {
        get
        {
            if (states.Count == 0)
            {
                return "";
            }

            return _currentDescription;
        }
        private set => _currentDescription = value;
    }

    public List<string> states;
    public int currectIndex = 0;

    private void Start()
    {
        _currentDescription = states[currectIndex];
    }

    public void SetNextState()
    {
        currectIndex = (currectIndex + 1) % states.Count;
        CurrentDescription = states[currectIndex];
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        _animator.SetTrigger("Switch");
    }
}