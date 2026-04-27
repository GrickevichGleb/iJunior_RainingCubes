using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnerStats : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [Space] 
    [SerializeField] private TMP_Text _spawnedText;
    [SerializeField] private TMP_Text _instantiatedText;
    [SerializeField] private TMP_Text _activeText;

    private int _spawned = 0;
    private int _instantiated = 0;
    private int _active = 0;

    private void OnEnable()
    {
        _spawner.UpdateStats += OnUpdateStats;
    }

    private void Start()
    {
        Display();
    }

    private void OnDisable()
    {
        _spawner.UpdateStats -= OnUpdateStats;
    }

    private void OnUpdateStats(int instantiated, int active)
    {
        _spawned++;
        _instantiated = instantiated;
        _active = active;
        
        Display();
    }
    
    private void Display()
    {
        _spawnedText.text = _spawned.ToString();
        _instantiatedText.text = _instantiated.ToString();
        _activeText.text = _active.ToString();
    }
    
    
}
