using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelSystem : MonoBehaviour
{
    public event EventHandler OnExpreienceChanged;
    public event EventHandler OnLevelChanged;
    public event EventHandler OnAttributeSpent;
    public event EventHandler OnAllAttributesSpent;

    [Header("Systems")]
    public int level;
    [Tooltip("The current experience of the player")]
    public float experience;
    [Tooltip("The experience needed for the next level")]
    public float experienceToNextLevel;
    [Tooltip("The experience scale factor")]
    public float experienceScaleFactor;

    public int attributePoints;

    [Header("Player Stats")]
    public int extraMaxLives;
    public int extraJump;
    public int extraSpeed;

    private ExpBar expBar;

	private void Awake()
	{
        expBar = FindObjectOfType<ExpBar>();
    }
    public void AddExperience(float enemyExperience)
    {
        experience += enemyExperience;


        while (experience >= experienceToNextLevel)
        {
            level++;
            attributePoints++;
            experience -= experienceToNextLevel;
            CalculateNextLevel();
            if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
            
        }
        if (OnExpreienceChanged != null) OnExpreienceChanged(this, EventArgs.Empty);
    }

    void LevelUp()
    {
        level++;
        CalculateNextLevel();
        if (experience >= experienceToNextLevel)
        {
            LevelUp();
        }
        else return;
    }

    void CalculateNextLevel()
    {

        experienceToNextLevel *= Mathf.Pow((level + 1), experienceScaleFactor);
        return;
    }

    public int GetLevelNumber()
	{
        return level;
	}
    public int GetAttributePoints()
	{
        return attributePoints;
	}

    public float GetExperienceNormalized()
	{
        return experience / experienceToNextLevel;
	}

    public void SpendAttribute()
	{
        attributePoints--;
        if(OnAttributeSpent != null) OnAttributeSpent(this, EventArgs.Empty);
        if(OnAllAttributesSpent != null && attributePoints == 0) OnAllAttributesSpent(this, EventArgs.Empty);
    }
}
