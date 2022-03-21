using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpWindow : UIelement
{
	private TextMeshProUGUI levelText;
	private TextMeshProUGUI attributePoints;
	private LevelSystem _LevelSystem;

	private void Awake()
	{
		_LevelSystem = FindObjectOfType<LevelSystem>();
		levelText = transform.Find("levelText").Find("LevelText").GetComponent<TextMeshProUGUI>();
		attributePoints = transform.Find("AttributePoints").Find("AttributePointsText").GetComponent<TextMeshProUGUI>();
	}

	private	void SetLevelNumber (int levelNumber)
	{
		levelText.text = "LEVEL\n" + (levelNumber + 1);
	}

	private void OnEnable()
	{
		SetLevelNumber(_LevelSystem.GetLevelNumber());
		SetAtrributePoints(_LevelSystem.GetAttributePoints());
		_LevelSystem.OnAttributeSpent += _LevelSystem_OnAttributeSpent;

	}

	private void _LevelSystem_OnAttributeSpent(object sender, System.EventArgs e)
	{
		SetAtrributePoints(_LevelSystem.GetAttributePoints());
	}

	private void SetAtrributePoints (int attributePointsNumber)
	{
		attributePoints.text = "Attributes:\n" + (attributePointsNumber);
	}

}
