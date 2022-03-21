using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    public float enemyExperienceValue;
    private LevelSystem _LevelSystem;
    void Start()
    {
        _LevelSystem = FindObjectOfType<LevelSystem>();
    }

    public void OnDeath()
	{
        _LevelSystem.AddExperience(enemyExperienceValue);
	}

}
