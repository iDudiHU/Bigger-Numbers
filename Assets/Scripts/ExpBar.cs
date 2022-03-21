using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpBar : MonoBehaviour
{
	private Slider expSlider;
	private LevelSystem _LevelSystem;
	private void Awake()
	{
		_LevelSystem = FindObjectOfType<LevelSystem>();
		expSlider = gameObject.GetComponent<Slider>();
		_LevelSystem.OnExpreienceChanged += _LevelSystem_OnExpreienceChanged;
	}

	private void _LevelSystem_OnExpreienceChanged(object sender, System.EventArgs e)
	{
		SetExperienceBarSize(_LevelSystem.GetExperienceNormalized());
	}

	private void SetExperienceBarSize(float experienceNormalized)
	{
		expSlider.value = experienceNormalized;
	}



}
