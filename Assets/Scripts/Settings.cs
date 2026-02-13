using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private int _vSyncCoeff = 0;
    [SerializeField] private int _targetFrameRate = 30;
    private void Start()
    {
        QualitySettings.vSyncCount = _vSyncCoeff;
        Application.targetFrameRate = _targetFrameRate;
    }
    
}
